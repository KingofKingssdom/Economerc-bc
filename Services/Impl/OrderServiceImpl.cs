using Ecommerce.Data;
using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;
using Ecommerce.Models;
using Ecommerce.Responses.Enum;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services.Impl
{
    public class OrderServiceImpl : IOrderService
    {
        private readonly MyAppContext _context;
        public OrderServiceImpl(MyAppContext context)
        {
            _context = context;
        }
        public async Task<ResOrderDto> CreateOrder(long userId, ReqOrderDto reqOrderDto)
        {
            User user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new Exception($"User have id = {userId} not found");
            }
            var cartItemsInDb = await _context.CartItems
                .Include(ci => ci.ProductVariant)
                .Include(ci => ci.Cart)
                .Where(ci => reqOrderDto.SelectedCartItemIds.Contains(ci.Id))
                .ToListAsync();
            var validItems = cartItemsInDb
                .Where(ci => ci.Cart != null && ci.Cart.UserId == userId)
                .ToList();
            if (!validItems.Any())
            { 
                throw new Exception($"Xác thực giỏ hàng thất bại");
            }
            Random rd = new Random();
            int randomNumber = rd.Next(1000, 10000);
            Order order = new Order()
            {
                OrderCode = $"DH-{randomNumber}-{DateTime.Now}",
                DayCreate = DateTime.Now,
                OrderStatus = OrderStatus.PENDING,
                PaymentMethod = reqOrderDto.PaymentMethod,
                PaymentStatus = (reqOrderDto.PaymentMethod == PaymentMethod.COD)
                         ? PaymentStatus.UNPAID : PaymentStatus.PAID,
                ReceiverName = reqOrderDto.ReceiverName,
                ReceiverPhone = reqOrderDto.ReceiverPhone,
                ShippingAddress = reqOrderDto.ShippingAddress,
                UserId = user.Id,
                OrderItems = new List<OrderItem>()
            };
            double total = 0;
            foreach (var cartItem in validItems) // Duyệt trên validItems để an toàn
            {
                // Lấy giá từ ProductVariant (CurrentPrice)
                var price = cartItem.TotalPrice;

                OrderItem orderItem = new OrderItem()
                {
                    ProductVariantId = cartItem.ProductVariantId,
                    Quantity = cartItem.Quantity,
                    PriceAtTime = price,
                    CreateAt = DateTime.Now,
                };

                total += price;
                order.OrderItems.Add(orderItem);
            }
            order.TotalPrice = total;
            await _context.Orders.AddAsync(order);
            _context.CartItems.RemoveRange(validItems);
            await _context.SaveChangesAsync();
            return new ResOrderDto()
            {
                Id = order.Id,
                OrderCode = order.OrderCode,
                PaymentMethod = order.PaymentMethod,
                PaymentStatus = order.PaymentStatus,
                DayCreate = order.DayCreate,
                TotalPrice = order.TotalPrice,
                ShippingAddress = order.ShippingAddress,
                ReceiverName = order.ReceiverName,
                ReceiverPhone = order.ReceiverPhone,
                OrderStatus = order.OrderStatus
            };
        }
        public async Task<List<ResOrderDto>> GetAllOrderByUserId(long userId)
        {
            List<Order> orders = await _context.Orders
                .Where(o => o.UserId == userId).ToListAsync();
            var result = orders.Select(o => new ResOrderDto()
            {
                Id = o.Id,
                OrderCode = o.OrderCode,
                OrderStatus = o.OrderStatus,
                PaymentMethod = o.PaymentMethod,
                PaymentStatus = o.PaymentStatus,
                DayCreate = o.DayCreate,
                TotalPrice = o.TotalPrice,
                ReceiverName = o.ReceiverName,
                ReceiverPhone = o.ReceiverPhone,
                ShippingAddress = o.ShippingAddress
            }).ToList();
            return result;
        }
        public async Task<ResOrderDto> UpdateOrderByOrderStatus(long orderId, OrderStatus newOrderStatus)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == orderId);
            if(order == null)
            {
                throw new Exception($"Order is not found");
            }
            order.OrderStatus = newOrderStatus;
            _context.Orders.UpdateRange(order);
            await _context.SaveChangesAsync();
            return new ResOrderDto()
            {
                OrderCode = order.OrderCode,
                OrderStatus = order.OrderStatus
            };
        }
        public async Task<ResOrderDto> CancelOrderByOrderId(long orderId)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o=> o.Id == orderId);
            if(order == null)
            {
                throw new Exception($"Order is not found");
            }
            order.OrderStatus = OrderStatus.CANCELED;
            await _context.SaveChangesAsync();
            return new ResOrderDto()
            {
                OrderCode = order.OrderCode,
                OrderStatus = order.OrderStatus
            };
        }
        public async Task<List<ResOrderDto>> GetAllOrder()
        {
            var orders = await _context.Orders.ToListAsync();
            var resOrders = orders.Select(o => new ResOrderDto()
            {
                Id = o.Id,
                OrderCode = o.OrderCode,
                OrderStatus = o.OrderStatus,
                PaymentMethod = o.PaymentMethod,
                PaymentStatus = o.PaymentStatus,
                DayCreate = o.DayCreate,
                TotalPrice = o.TotalPrice,
                ReceiverName = o.ReceiverName,
                ReceiverPhone = o.ReceiverPhone,
                ShippingAddress = o.ShippingAddress
            }).ToList();
            return resOrders;
        }
        
    }
}
