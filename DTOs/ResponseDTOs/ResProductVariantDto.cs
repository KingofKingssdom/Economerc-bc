namespace Ecommerce.DTOs.ResponseDTOs
{
    public class ResProductVariantDto
    {
        public long Id { set; get; }
        public string Storage { set; get; }
        public double OriginPrice { set; get; }
        public double CurrentPrice { set; get; }
        public long ProductId { set; get; }
        public string ColorName { set; get; }
        public string UrlProductColor { set; get; }
        public int Stock { set; get; }
    }
}
