using Ecommerce.Models;

namespace Ecommerce.DTOs.ResponseDTOs
{
    public class ResBrandDto
    {
        public long Id { set; get; }
        public string BrandCode { set; get; }
        public string BrandName { set; get; }
        public string UrlImageBrand { set; get; }
        public List<ResCategoryDto> Categories { set; get; } = new List<ResCategoryDto>();
    }
}
