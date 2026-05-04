namespace Ecommerce.DTOs.RequestDTOs
{
    public class ReqProductVariantDto
    {
        public string Storage { set; get; }
        public double OriginPrice { set; get; }
        public double CurrentPrice { set; get; }
        public string ColorName { set; get; }
        public IFormFile UrlProductColor { set; get; }
        public long ProductId { set; get; }
        public int Stock { set; get; }
    }
}
