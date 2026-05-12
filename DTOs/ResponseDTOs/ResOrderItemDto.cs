using Ecommerce.Models;

namespace Ecommerce.DTOs.ResponseDTOs
{
    public class ResOrderItemDto
    {
        public long OrderId { set; get; }
        public long ProductVariantId { set; get; }
        public double PriceAtTime { set; get; }
        public int Quantity { set; get; }
        public DateTime CreateAt { set; get; }
        public Order Order { set; get; }
        public ProductVariant ProductVariant { set; get; }
        public required string ProductName { set; get; }
        public string UrlProductColor { set; get; }
        public string ColorName { set; get; }
        public double TotalPrice { set; get; }
        public string Storage { set; get; }
        public string ReceiverName { set; get; }
        public string ReceiverPhone { set; get; }
        public string ShippingAddress { set; get; }
    }
}
