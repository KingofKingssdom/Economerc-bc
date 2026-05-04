namespace Ecommerce.Models
{
    public class CartItem
    {
        public long Id { set; get; }
        public long CartId { set; get; }
        public long ProductVariantId { set; get; }
        public double PriceAtTime { set; get; }
        public int Quantity { set; get; }
        public double TotalPrice { set; get; }
        public DateTime CreateAt { set; get; }
        public Cart Cart { set; get; }
        public ProductVariant ProductVariant { set; get; }

    }
}
