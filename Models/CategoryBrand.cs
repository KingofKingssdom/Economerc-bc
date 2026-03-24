namespace Ecommerce.Models
{
    public class CategoryBrand
    {
        public long CategoryId { set; get; }
        public Category Category { set; get; } = null!;
        public long BrandId { set; get; }
        public Brand Brand { set; get; } = null!;
    }
}
