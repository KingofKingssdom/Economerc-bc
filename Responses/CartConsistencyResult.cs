using Ecommerce.DTOs.RequestDTOs;

namespace Ecommerce.Responses
{
    public class CartConsistencyResult
    {
        public bool IsValid { get; set; }
        public List<string> Errors { get; set; }
        public List<ReqCartItemUpdateDto> Changes { get; set; }
    }
}
