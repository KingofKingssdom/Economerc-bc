namespace Ecommerce.DTOs.ResponseDTOs
{
    public class ResProductVariantDto
    {
        public long Id { set; get; }
        public string Storage { set; get; }
        public double OriginPrice { set; get; }
        public double CurrentPrice { set; get; }
        public long ProductId { set; get; }
    }
}
