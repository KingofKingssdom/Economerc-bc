namespace Ecommerce.DTOs.ResponseDTOs
{
    public class ResProductVariantDto
    {
        public long Id { set; get; }
        public string Storage { set; get; }
        public double PriceOrigin { set; get; }
        public double PriceDiscount { set; get; }
        public long ProductId { set; get; }
    }
}
