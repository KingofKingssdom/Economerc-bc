using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;
using Ecommerce.Responses.Enum;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles ="Admin, Customer")]
        public async Task<ActionResult<ResOrderDto>> Create(long userId, [FromBody] ReqOrderDto reqOrderDto)
        {
            if (reqOrderDto.SelectedCartItemIds == null || !reqOrderDto.SelectedCartItemIds.Any())
            {
                return BadRequest("Vui lòng chọn ít nhất một sản phẩm để thanh toán.");
            }
            Console.WriteLine("JSON Received ID Count: " + reqOrderDto.SelectedCartItemIds?.Count);
            var order = await _orderService.CreateOrder(userId, reqOrderDto);
            return Ok(new
            {
                message = "Created Success",
                data = order
            });

        }
        [HttpGet("{userId}")]
        [Authorize(Roles ="Admin, Customer")]
        public async Task<ActionResult<List<ResOrderDto>>> GetAllOrderByUserId(long userId)
        {
            var orders = await _orderService.GetAllOrderByUserId(userId);
            return Ok( new
            {
                message = "Get all success",
                data = orders
            });
        }
        [HttpPut("orderId/{orderId}/newOrderStatus/{newOrderStatus}")]
        [Authorize( Roles = "Admin, Customer")]
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
        [Authorize(Roles ="Admin")]
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

        [HttpGet]
        [Authorize(Roles = "Admin, Customer")]
        public async Task<ActionResult> GetAll()
        {
            var resOrders = await _orderService.GetAllOrder();
            return Ok(new
            {
                message = "Data is retrieved successful",
                data = resOrders
            });
        }
        [HttpGet("count")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Count()
        {
            var order = await _orderService.CountOrder();
            return Ok(new
            {
                message = "Order counted successfully",
                data = order
            });
        }
        [HttpGet("total-prices/{orderStatus}")]
        [Authorize (Roles = "Admin")]
        public async Task<ActionResult> TotalPrice(OrderStatus orderStatus)
        {
            var total = await _orderService.SumPriceOrder(orderStatus);
            return Ok(new
            {
                message = "Data counted successfully",
                data = total
            });
        }
        [HttpGet("orderStatus/{orderStatus}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetByOrderStatus(OrderStatus orderStatus)
        {
            try
            {
                var resOrder = await _orderService.GetOrderByOrderStatus(orderStatus);
                return Ok(new
                {
                    message = "Data is retrieved successfully",
                    data = resOrder
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            

        }
    }
}
