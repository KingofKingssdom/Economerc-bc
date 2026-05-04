using Ecommerce.Data;
using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;
using Ecommerce.Models;
using Ecommerce.Responses;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services.Impl
{
    public class CartItemServiceImpl : ICartItemService
    {
        private readonly MyAppContext _context;
        public CartItemServiceImpl(MyAppContext context)
        {
            _context = context;
        }
        public async Task<ResCartItemDto> CreateCartItem(ReqCartItemDto reqCartItemDto)
        {
            var productVariant = await _context.ProductVariants
                .FirstOrDefaultAsync(pv => pv.Id == reqCartItemDto.ProductVariantId);
            if (productVariant == null)
            {
                throw new Exception($"Không tìm thấy biến thể sản phẩm với ID: {reqCartItemDto.ProductVariantId}");
            }
            var priceCurrent = productVariant.CurrentPrice;
            var existingItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.CartId == reqCartItemDto.CartId
                                       && ci.ProductVariantId == reqCartItemDto.ProductVariantId);

            CartItem itemToReturn;
            if (existingItem != null)
            {
                existingItem.Quantity += reqCartItemDto.Quantity;
                existingItem.PriceAtTime = priceCurrent;
                _context.CartItems.Update(existingItem);
                itemToReturn = existingItem;
            }
            else
            {
                var cartItem = new CartItem()
                {
                    CartId = reqCartItemDto.CartId,
                    ProductVariantId = reqCartItemDto.ProductVariantId,
                    PriceAtTime = priceCurrent,
                    Quantity = reqCartItemDto.Quantity,
                    CreateAt = DateTime.Now,
                    TotalPrice = priceCurrent * reqCartItemDto.Quantity
                };
                 
                await _context.CartItems.AddAsync(cartItem);
                itemToReturn = cartItem;
            }
           
            await _context.SaveChangesAsync();
            var resCartItem = await _context.CartItems
                .Include(ci => ci.ProductVariant)
                .ThenInclude(pv => pv.Product)
                .FirstOrDefaultAsync(ci => ci.Id == itemToReturn.Id);
            if (resCartItem == null)
            {
                throw new Exception($"Product is not found");
            }
            return new ResCartItemDto()
            {
                CartId = resCartItem.CartId,
                ProductVariantId = resCartItem.ProductVariantId,
                PriceAtTime = resCartItem.PriceAtTime,
                Quantity = resCartItem.Quantity,
                CreateAt = resCartItem.CreateAt,
                ProductName = resCartItem.ProductVariant.Product.ProductName

            };

        }
        public async Task<List<ResCartItemDto>> GetCartItemByUserId(long userId)
        {
            Cart? cart =  await _context.Carts
                .FirstOrDefaultAsync(c => c.UserId == userId);
            if(cart == null)
            {
                throw new Exception($"Cart is user id ={userId} not found");
            }
            var cartItems = await _context.CartItems
                .Where(ci=> ci.CartId == cart.Id)
                .Include(ci=>ci.ProductVariant)
                .ThenInclude(pv=>pv.Product)
                .ToListAsync();

            var resCartItem = cartItems.Select(ci => new ResCartItemDto {
                Id = ci.Id,
                ProductVariantId = ci.ProductVariantId,
                PriceAtTime = ci.PriceAtTime,
                Quantity = ci.Quantity,
                CreateAt = ci.CreateAt,
                ProductName = ci.ProductVariant.Product.ProductName,
                UrlProductColor = ci.ProductVariant.UrlProductColor,
                ColorName = ci.ProductVariant.ColorName,
                TotalPrice = ci.TotalPrice,
                Storage = ci.ProductVariant.Storage

            }).ToList();
            return resCartItem;
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
            var resCartItem = await _context.CartItems
                .Include(ci => ci.ProductVariant)
                .ThenInclude(pv => pv.Product)
                .FirstOrDefaultAsync();
            if(resCartItem == null)
            {
                throw new Exception($"Product is not found in Cart");
            }
            return new ResCartItemDto()
            {
                CartId = resCartItem.CartId,
                ProductVariantId = resCartItem.ProductVariantId,
                PriceAtTime = resCartItem.PriceAtTime,
                Quantity = resCartItem.Quantity,
                CreateAt = resCartItem.CreateAt,
                ProductName = resCartItem.ProductVariant.Product.ProductName

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
                    ProductName = ci.ProductVariant.Product.ProductName
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

                if (variant == null)
                {
                    result.IsValid = false;
                    result.Errors.Add($"Sản phẩm '{variant?.Product.ProductName}' hiện không còn bán.");
                    continue;
                }

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
                    item.PriceAtTime = variant.CurrentPrice;
                }
            }

            if (!result.IsValid)
            {
                await _context.SaveChangesAsync(); 
            }

            return result;
        }
    }
}
