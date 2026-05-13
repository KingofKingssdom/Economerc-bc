namespace Ecommerce.DTOs.RequestDTOs
{
    public class ReqSpecificationMapping
    {
        public long ProductId { set; get; }
        public List<long> ProductSpecificationIds { set; get; }
    }
}
