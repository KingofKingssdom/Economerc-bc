using Ecommerce.Models;

namespace Ecommerce.DTOs.RequestDTOs
{
    public class ReqProductDto
    {
        public required string ProductCode { set; get; }
        public required string ProductName { set; get; }
        public string? Description { set; get; }
        public required IFormFile UrlImageProduct { set; get; }
        public bool IsFeatured { set; get; }
        public bool IsOnPromotion { set; get; }
        public long BrandId { set; get; }
        public long CategoryId { set; get; }
        public required List<long> ProductSpecificationId { set; get; }


    }
}
