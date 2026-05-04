using Ecommerce.DTOs.ResponseDTOs;

namespace Ecommerce.Services
{
    public interface IOrderItemService
    {
        public Task<List<ResOrderItemDto>> GetAllOrderItemsByOrderId(long orderId);
    }
}
