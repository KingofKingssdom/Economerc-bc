using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;
using Ecommerce.Responses;

namespace Ecommerce.Services
{
    public interface ICartItemService
    {
        public Task<ResCartItemDto> CreateCartItem(ReqCartItemDto reqCartItemDto);
        public Task<List<ResCartItemDto>> GetCartItemByUserId(long userId);
        public Task<ResCartItemDto> UpdateCartItemByQuantity(long cartItemId, int newQuantity);
        public Task<List<ResCartItemDto>> DeleteCartItemById(List<long> cartItemIds, long userId);
        public Task<bool> DeleteAllCartItem(long userId);
        public Task<CartConsistencyResult> CheckCartConsistency(long userId);
        //public Task<decimal> GetTotalCartValue(int userId);
    }
}
