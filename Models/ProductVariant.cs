namespace Ecommerce.Models
{
    public class ProductVariant
    {
        public long Id { set; get; }
        public string Storage { set; get; }
        public double OriginPrice { set; get; }
        public double CurrentPrice { set; get; }
        public long ProductId { set; get; }
        public Product Product { set; get; } = null!;
        public long ProductColorId { set; get; }
        public ProductColor ProductColor { set; get; }
        public int Stock { set; get; }

    }
}
