namespace Ecommerce.DTOs.RequestDTOs
{
    public class ReqCartItemDto
    {
        public long CartId { set; get; }
        public long ProductVariantId { set; get; }
        public double PriceAtTime { set; get; }
        public int Quantity { set; get; }
 
        public DateTime CreateAt { set; get; }
       
    }
}
