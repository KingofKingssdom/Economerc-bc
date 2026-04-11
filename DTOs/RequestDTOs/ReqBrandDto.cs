namespace Ecommerce.DTOs.RequestDTOs
{
    public class ReqBrandDto
    {
        public string BrandCode { set; get; }
        public string BrandName { set; get; }
        public IFormFile? UrlImageBrand { set; get; }
        public List<long> CategoryIds { set; get; }
    }
}
