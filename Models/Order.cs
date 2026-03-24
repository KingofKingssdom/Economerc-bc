using Ecommerce.Responses.Enum;

namespace Ecommerce.Models
{
    public class Order
    {
        public long Id { set; get; }
        public OrderStatus OrderStatus { set; get; }
        public PaymentStatus PaymentStatus { set; get; }
        public PaymentMethod PaymentMethod { set; get; }
        public DateTime DayCreate { set; get; }
        public double TotalPrice { set; get; }
        public string OrderName { set; get; }

        public List<OrderItem> OrderItems { get; } = new List<OrderItem>();
    }
}
