namespace Ecommerce.Models
{
    public class CartItem
    {
        public long Id { set; get; }
        public long CartId { set; get; }
        public long CategoryCode { set; get; }
        public double ProductPrice { set; get; }
        public int Quantity { set; get; }
        public double TotalPrice { set; get; }
        public Cart Cart { set; get; }

        public long ProductId { set; get; }
        public Product Product { set; get; }
        public long ProductColorId { set; get; }
        public ProductColor ProductColor { set; get; }
        public long ProductVariantId { set; get; }
        public ProductVariant ProductVariant { set; get; }

    }
}
