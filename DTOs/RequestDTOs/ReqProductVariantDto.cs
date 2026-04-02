namespace Ecommerce.DTOs.RequestDTOs
{
    public class ReqProductVariantDto
    {
        public string Storage { set; get; }
        public double PriceOrigin { set; get; }
        public double PriceDiscount { set; get; }
        public long ProductId { set; get; }
    }
}
