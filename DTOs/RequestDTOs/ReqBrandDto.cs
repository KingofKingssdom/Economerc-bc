namespace Ecommerce.DTOs.RequestDTOs
{
    public class ReqBrandDto
    {
        public string? brandCode { set; get; }
        public string? brandName { set; get; }
        public IFormFile? UrlImageBrand { set; get; }
    }
}
