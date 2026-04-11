namespace Ecommerce.DTOs.ResponseDTOs
{
    public class ResCartItemDto
    {
        public long Id { set; get; }
        public long CartId { set; get; }
        public long ProductVariantId { set; get; }
        public double PriceAtTime { set; get; }
        public int Quantity { set; get; }
        public DateTime CreateAt { set; get; }
        //public ResProductVariantDto resProductVariantDto { set; get; }
        public required string ProductName { set; get; }
        public required string ProductColor { set; get; }
        public required string UrlImageProduct { set; get; }

    }
}
