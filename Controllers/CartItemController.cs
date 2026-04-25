using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/cartItem")]
    public class CartItemController: ControllerBase
    {
        private readonly ICartItemService _cartItemService;
        public CartItemController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }
        [HttpPost("create")]

        public async Task<ActionResult<ResCartItemDto>> Create(ReqCartItemDto reqCartItemDto)
        {
            var cartItem = await _cartItemService.CreateCartItem(reqCartItemDto);
            return Ok(new
            {
                message = "Create item in cart success",
                data = cartItem,
            });
        }
        

    }
}
