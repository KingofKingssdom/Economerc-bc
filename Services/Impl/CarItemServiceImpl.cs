using Ecommerce.Data;
using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;
using Ecommerce.Models;
using Ecommerce.Responses;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services.Impl
{
    public class CarItemServiceImpl : ICartItemService
    {
        private readonly MyAppContext _context;
        public CarItemServiceImpl(MyAppContext context)
        {
            _context = context;
        }
        public async Task<ResCartItemDto> CreateCartItem(ReqCartItemDto reqCartItemDto)
        {
            var existingItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.CartId == reqCartItemDto.CartId
                                       && ci.ProductVariantId == reqCartItemDto.ProductVariantId);
            if(existingItem != null)
            {
                existingItem.Quantity += reqCartItemDto.Quantity;
                existingItem.PriceAtTime = reqCartItemDto.PriceAtTime;
                _context.CartItems.Update(existingItem);
            }

            CartItem cartItem = new CartItem()
            {
                CartId = reqCartItemDto.CartId,
                ProductVariantId = reqCartItemDto.ProductVariantId,
                PriceAtTime = reqCartItemDto.PriceAtTime,
                Quantity = reqCartItemDto.Quantity,
                CreateAt = DateTime.Now
            };
                await _context.CartItems.AddAsync(cartItem);
            
            await _context.SaveChangesAsync();
            return new ResCartItemDto()
            {
                CartId = cartItem.CartId,
                ProductVariantId = cartItem.ProductVariantId,
                PriceAtTime = cartItem.PriceAtTime,
                Quantity = cartItem.Quantity,
                CreateAt = cartItem.CreateAt
            };

        }
        public async Task<List<ResCartItemDto>> GetCartItemByUserId(long userId)
        {
            Cart cart =  await _context.Carts
                .FirstOrDefaultAsync(c => c.UserId == userId);
            if(cart == null)
            {
                throw new Exception($"Cart is user id ={userId} not found");
            }
            var cartItems = await _context.CartItems
                .Where(ci=> ci.Id == cart.Id)
                .Include(pv=>pv.ProductVariant)
                .ToListAsync();

            var result = cartItems.Select(ci => new ResCartItemDto { 
                Id = ci.Id,
                ProductVariantId = ci.ProductVariantId,
                PriceAtTime = ci.PriceAtTime,
                Quantity = ci.Quantity,
                CreateAt = ci.CreateAt
            
            }).ToList();
            return result;
        }
        public async Task<ResCartItemDto> UpdateCartItemByQuantity(long cartItemId, int newQuantity)
        {
            var item = await _context.CartItems
                .Include(ci => ci.ProductVariant)
                .FirstOrDefaultAsync(ci => ci.Id == cartItemId);
            if(item == null)
            {
                throw new Exception($"CartItem is cartIteam id ={cartItemId} not found");
            }
            if(newQuantity <= 0)
            {
                _context.CartItems.Remove(item);
            }
            else
            {
                if(newQuantity > item.ProductVariant.Stock)
                {
                    throw new Exception($"Item number is over stock number");
                }
                item.Quantity = newQuantity;
                _context.CartItems.Update(item);

            }
            await _context.SaveChangesAsync();
            return new ResCartItemDto()
            {
                CartId = item.CartId,
                ProductVariantId = item.ProductVariantId,
                PriceAtTime = item.PriceAtTime,
                Quantity = item.Quantity,
                CreateAt = item.CreateAt
            };
        }
        public async Task<List<ResCartItemDto>> DeleteCartItemById(List<long> cartItemIds, long userId)
        {
            var itemsToDelete = await _context.CartItems
                    .Where(ci => cartItemIds.Contains(ci.Id) && ci.Cart.UserId == userId)
                    .ToListAsync();
            if (itemsToDelete.Any())
            {
                
                _context.CartItems.RemoveRange(itemsToDelete);
                await _context.SaveChangesAsync();
            }

            var remainingItems = await _context.CartItems
                .Where(ci => ci.Cart.UserId == userId)
                .Include(ci => ci.ProductVariant)
                .ThenInclude(pv => pv.Product)
                .Select(ci => new ResCartItemDto
                {
                    Id = ci.Id,
                    ProductVariantId = ci.ProductVariantId,
                    Quantity = ci.Quantity,
                    PriceAtTime = ci.PriceAtTime,
                    
                })
                .ToListAsync();

            return remainingItems;
        }
        public async Task<bool> DeleteAllCartItem(long userId)
        {
            var cart = await _context.Carts
        .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null) return false;

           
            var items = await _context.CartItems
                .Where(ci => ci.CartId == cart.Id)
                .ToListAsync();

            if (items.Any())
            {
                _context.CartItems.RemoveRange(items);
                await _context.SaveChangesAsync();
            }

            return true;
        }
        public async Task<CartConsistencyResult> CheckCartConsistency(long userId)
        {
            var result = new CartConsistencyResult
            {
                IsValid = true,
                Errors = new List<string>(),
                Changes = new List<ReqCartItemUpdateDto>()
            };

            var cartItems = await _context.CartItems
                .Include(ci => ci.ProductVariant)
                    .ThenInclude(pv => pv.Product)
                .Where(ci => ci.Cart.UserId == userId)
                .ToListAsync();

            foreach (var item in cartItems)
            {
                var variant = item.ProductVariant;

                // 1. Kiểm tra sản phẩm còn kinh doanh không
                if (variant == null)
                {
                    result.IsValid = false;
                    result.Errors.Add($"Sản phẩm '{variant?.Product.ProductName}' hiện không còn bán.");
                    continue;
                }

                // 2. Kiểm tra tồn kho
                if (item.Quantity > variant.Stock)
                {
                    result.IsValid = false;
                    result.Changes.Add(new ReqCartItemUpdateDto
                    {
                        CartItemId = item.Id,
                        ChangeType = "StockReduced",
                        Message = $"Sản phẩm '{variant.Product.ProductName}' chỉ còn {variant.Stock} món trong kho.",
                        NewStock = variant.Stock
                    });
                }

                // 3. Kiểm tra biến động giá (So sánh giá trong giỏ và giá hiện tại của Variant)
                // Lưu ý: CurrentPrice là giá sau cùng (đã tính sale) như mình đã bàn ở trên
                if (item.PriceAtTime != variant.CurrentPrice)
                {
                    result.IsValid = false;
                    result.Changes.Add(new ReqCartItemUpdateDto
                    {
                        CartItemId = item.Id,
                        ChangeType = "PriceChanged",
                        OldPrice = item.PriceAtTime,
                        NewPrice = variant.CurrentPrice,
                        Message = $"Giá của '{variant.Product.ProductName}' đã thay đổi."
                    });

                    // Tự động cập nhật lại giá mới vào giỏ hàng để đồng bộ
                    item.PriceAtTime = variant.CurrentPrice;
                }
            }

            if (!result.IsValid)
            {
                await _context.SaveChangesAsync(); // Lưu lại các cập nhật giá/kho mới nhất vào giỏ
            }

            return result;
        }
    }
}
