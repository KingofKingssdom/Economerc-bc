namespace Ecommerce.Models
{
    public class Product
    {
        public long Id { set; get; }
        public required string ProductCode { set; get; }
        public required string ProductName { set; get; }
        public string? Description { set; get; }
        public required string UrlImageProduct { set; get; }

        public bool IsFeatured { set; get; }
        public bool IsOnPromotion { set; get; }
        public long BrandId { set; get; }
        public Brand Brand { set; get; } = null!;
        public long CategoryId { set; get; }
        public Category Category { set; get; } = null!;
        public List<ProductColor> ProductColors { get; } = new List<ProductColor>();
        public List<ProductVariant> ProductVariants { get; } = new List<ProductVariant>();
        public List<ProductSpecificationMapping> ProductSpecifications { get; } = [];
        public List<SpecificationDetail> SepcificationDetail { get; } = new List<SpecificationDetail>(); 
     }
}
