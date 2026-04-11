using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;
using Ecommerce.Responses.Enum;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController: ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost("{userId}")]
        public async Task<ActionResult<ResOrderDto>> Create(long userId, ReqOrderDto reqOrderDto)
        {
            var order = await _orderService.CreateOrder(userId, reqOrderDto);
            return Ok(new
            {
                message = "Created Success",
                data = order
            });

        }
        [HttpGet("{userId}")]
        public async Task<ActionResult<List<ResOrderDto>>> GetAllOrderByUserId(long userId)
        {
            var orders = await _orderService.GetAllOrderByUserId(userId);
            return Ok( new
            {
                message = "Get all success",
                data = orders
            });
        }
        [HttpPut("{orderId}/{newOrderStatus}")]
        public async Task<ActionResult<ResOrderDto>> UpdateOrderByOrderStatus(long orderId, OrderStatus newOrderStatus)
        {
            try
            {
                var order = await _orderService.UpdateOrderByOrderStatus(orderId, newOrderStatus);
                return Ok(new
                {
                    message = "Update success",
                    data = order
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
        [HttpPut("{orderId}")]
        public async Task<ActionResult<ResOrderDto>> CancelOrderByUserId(long orderId)
        {
            try
            {
                var order = await _orderService.CancelOrderByOrderId(orderId);
                return Ok(new
                {
                    message = "Update success",
                    data = order
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
           
        }
    }
}
