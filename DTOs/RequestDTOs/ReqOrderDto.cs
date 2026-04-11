using Ecommerce.Models;
using Ecommerce.Responses.Enum;

namespace Ecommerce.DTOs.RequestDTOs
{
    public class ReqOrderDto
    {
        public string OrderCode { set; get; }
        public OrderStatus OrderStatus { set; get; }
        public PaymentStatus PaymentStatus { set; get; }
        public PaymentMethod PaymentMethod { set; get; }
        public DateTime DayCreate { set; get; }
        public double TotalPrice { set; get; }
        public string ReceiverName { set; get; }
        public string ReceiverPhone { set; get; }
        public string ShippingAddress { set; get; }
        public List<ReqCartItemDto> ReqCartItemDtos { set; get; } = new List<ReqCartItemDto>();
        public List<OrderItem> OrderItems { get; } = new List<OrderItem>();
    }
}
