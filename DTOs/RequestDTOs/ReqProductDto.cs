namespace Ecommerce.DTOs.RequestDTOs
{
    public class ReqProductDto
    {
        public string ProductCode { set; get; }
        public string ProductName { set; get; }
        public string Description { set; get; }
        public IFormFile UrlImageProduct { set; get; }
        public bool IsFeatured { set; get; }
        public bool IsOnPromotion { set; get; }
        public long BrandId { set; get; }
        public long CategoryId { set; get; }
    }
}
