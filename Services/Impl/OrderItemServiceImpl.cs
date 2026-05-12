using Ecommerce.Data;
using Ecommerce.DTOs.ResponseDTOs;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services.Impl
{
    public class OrderItemServiceImpl: IOrderItemService
    {
        private readonly MyAppContext _context;
        public OrderItemServiceImpl (MyAppContext context)
        {
            _context = context;
        }
            public async Task<List<ResOrderItemDto>> GetAllOrderItemsByOrderId(long orderId)
        {
            List<OrderItem> orderItems = await _context.OrderItems
                .Include(o=> o.Order)
                .Include(ci => ci.ProductVariant)
                .ThenInclude(pv => pv.Product)
                .Where(oi => oi.OrderId == orderId)
                .ToListAsync();
            var resOderItems = orderItems.Select(oi => new ResOrderItemDto
            {
                ColorName = oi.ProductVariant.ColorName,
                ProductName = oi.ProductVariant.Product.ProductName,
                Quantity = oi.Quantity,
                Storage = oi.ProductVariant.Storage,
                UrlProductColor = oi.ProductVariant.UrlProductColor,
                PriceAtTime = oi.PriceAtTime,
                TotalPrice = oi.Order.TotalPrice,
                ReceiverName = oi.Order.ReceiverName,
                ShippingAddress = oi.Order.ShippingAddress,
                ReceiverPhone = oi.Order.ReceiverPhone
            }).ToList();
            return resOderItems;
        }
    }
}
