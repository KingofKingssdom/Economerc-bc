namespace Ecommerce.Models
{
    public class OrderItem
    {
        public long Id { set; get; }
        public long CategoryCode { set; get; }
        public double PriceBuy { set; get; }
        public int Quantity { set; get; }
        public long OrderId { set; get; }
        public Order Order { set; get;  } 
        public long ProductId { set; get; }
        public Product Product { set; get; }
        public long ProductColorId { set; get; }
        public ProductColor ProductColor { set; get; }
        public long ProductVariantId { set; get; }
        public ProductVariant ProductVariant { set; get; }
    }
}
