namespace Ecommerce.Models
{
    public class ProductSpecificationMapping
    {
        public long ProductId { set; get; }
        public Product Product { set; get; }
        public long ProductSpecificationId { set; get; }
        public ProductSpecification ProductSpecification { set; get; }
    }
}
