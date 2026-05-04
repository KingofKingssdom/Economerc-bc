using Ecommerce.Responses.Enum;

namespace Ecommerce.Models
{
    public class Order
    {
        public long Id { set; get; }
        public string OrderCode { set; get; }
        public OrderStatus OrderStatus { set; get; }
        public PaymentStatus PaymentStatus { set; get; }
        public PaymentMethod PaymentMethod { set; get; }
        public DateTime DayCreate { set; get; }
        public double TotalPrice { set; get; }
        public long UserId { set; get; }
        public User User { set; get; } = null!;
        public string ReceiverName { set; get; }
        public string ReceiverPhone { set; get; }
        public string ShippingAddress { set; get; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
