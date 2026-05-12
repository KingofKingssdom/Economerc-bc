using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;
using Ecommerce.Responses.Enum;

namespace Ecommerce.Services
{
    public interface IOrderService
    {
        public Task<ResOrderDto> CreateOrder(long userId, ReqOrderDto reqOrderDto);
        public Task<List<ResOrderDto>> GetAllOrderByUserId(long userId);
        public Task<ResOrderDto> UpdateOrderByOrderStatus(long orderId, OrderStatus newOrderStatus);
        public Task<ResOrderDto> CancelOrderByOrderId(long orderId);
        public Task<List<ResOrderDto>> GetAllOrder();
        public Task<long> CountOrder();
        public Task<double> SumPriceOrder(OrderStatus orderStatus);
             
    }
}
