using Ecommerce.DTOs.ResponseDTOs;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/order-items")]
    [Authorize(Roles = "Admin, Customer")]
    public class OrderItemController:ControllerBase
    {
        private readonly IOrderItemService _orderItemService;
        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }
        [HttpGet("orderId/{orderId}")]
        public async Task<ActionResult<ResOrderItemDto>> GetAllByOrderId(long orderId)
        {
            var resOrderItem = await _orderItemService.GetAllOrderItemsByOrderId(orderId);
            return Ok(new
            {
                message = "Success",
                data = resOrderItem
            });
        }
    }
}
