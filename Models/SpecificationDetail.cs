namespace Ecommerce.Models
{
    public class SpecificationDetail
    {
        public long Id { set; get; }
        public string LableSpecification { set; get; }
        public string ValueSpecification { set; get; }
        public long ProductId { set; get; }
        public Product Product { set; get; }
    }
}
