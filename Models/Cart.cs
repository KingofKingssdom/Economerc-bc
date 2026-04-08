namespace Ecommerce.Models
{
    public class Cart
    {
        public long Id { set; get; }
        public long UserId { set; get; }
        public User User { set; get; }
        public List<CartItem> CartItem { get; } = new List<CartItem>();
    }
}
