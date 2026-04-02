namespace Ecommerce.DTOs.RequestDTOs
{
    public class ReqProductColorDto
    {
        public string ColorName { set; get; }
        public IFormFile UrlProductColor { set; get; }
        public long ProductId { set; get; }
    }
}
