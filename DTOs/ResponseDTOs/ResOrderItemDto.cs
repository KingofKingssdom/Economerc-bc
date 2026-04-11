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
    }
}
