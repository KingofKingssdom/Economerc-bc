using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/cart-items")]
    [Authorize(Roles ="Admin, Customer")]
    public class CartItemController: ControllerBase
    {
        private readonly ICartItemService _cartItemService;
        public CartItemController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }
        [HttpPost]
        public async Task<ActionResult<ResCartItemDto>> Create(ReqCartItemDto reqCartItemDto)
        {
            var cartItem = await _cartItemService.CreateCartItem(reqCartItemDto);
            return Ok(new
            {
                message = "Create item in cart success",
                data = cartItem,
            });
        }
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<ResCartItemDto>> GetAllByUserId(long userId)
        {
            var cartItem = await _cartItemService.GetCartItemByUserId(userId);
            return Ok(new
            {
                message = "Get success",
                data = cartItem
            });
        }

    }
}
