using Ecommerce.DTOs.ResponseDTOs;
using Ecommerce.Models;
using Ecommerce.Responses.Enum;

namespace Ecommerce.DTOs.RequestDTOs
{
    public class ReqOrderDto
    {
        public long UserId { set; get; }
        public List<long> SelectedCartItemIds { get; set; }
        public PaymentMethod PaymentMethod { set; get; }
        public string ReceiverName { set; get; }
        public string ReceiverPhone { set; get; }
        public string ShippingAddress { set; get; }
        public List<ResCartItemDto> ResCartItemDtos { set; get; } = new List<ResCartItemDto>();
    }
}
