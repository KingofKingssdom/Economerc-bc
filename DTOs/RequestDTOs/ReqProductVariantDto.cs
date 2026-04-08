namespace Ecommerce.DTOs.RequestDTOs
{
    public class ReqProductVariantDto
    {
        public string Storage { set; get; }
        public double OriginPrice { set; get; }
        public double CurrentPrice { set; get; }
        public long ProductId { set; get; }
    }
}
