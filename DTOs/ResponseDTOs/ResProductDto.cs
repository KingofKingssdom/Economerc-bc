namespace Ecommerce.DTOs.ResponseDTOs
{
    public class ResProductDto
    {
        public long Id { set; get; }
        public string ProductCode { set; get; }
        public string ProductName { set; get; }
        public string Description { set; get; }
        public string UrlImageProduct { set; get; }
        public bool IsFeatured { set; get; }
        public bool IsOnPromotion { set; get; }
        public long BrandId { set; get; }
        public long CategoryId { set; get; }
        public ResBrandDto ResBrandDto { set; get; }
        public ResCategoryDto ResCategory { set; get; } 
        public List<ResProductVariantDto> ResProductVariantDto { set; get; }
        public List<ResProductSpecificationDto> ResProductSpecification { set; get; }
    }
}
