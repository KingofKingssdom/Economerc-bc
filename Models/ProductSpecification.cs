namespace Ecommerce.Models
{
    public class ProductSpecification
    {
        public long Id { set; get; }
        public string SpecificationName { set; get; }
        public List<ProductSpecificationMapping> ProductSpecifications { get; } = [];
    }
}
