namespace Ecommerce.Models
{
    public class ProductColor
    {
        public long Id { set; get; }
        public string UrlProductColor { set; get; }
        public string ColorName { set; get; }
        public long ProductId { set; get; }
        public Product Products { get; } = null!;
        public List<ProductVariant> ProductVariants { get; } = new List<ProductVariant>();

    }
}
