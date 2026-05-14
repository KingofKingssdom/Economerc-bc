using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/payment")]
    [ApiController]
    [Authorize(Roles = "Admin, Customer")]
    public class PaymentController: ControllerBase
    {
        private readonly VNPayService _vnpayService;
        public PaymentController(VNPayService vnpayService)
        {
            _vnpayService = vnpayService;
        }
        [HttpGet("checkout/vnpay/{orderId}")]
        public async Task<IActionResult> Checkout(long orderId)
        {
            try
            {
                var url = await _vnpayService.CreatePaymentUrl(orderId, HttpContext);
                return Ok(new { paymentUrl = url });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("result-vnpay")]
        public async Task<IActionResult> Result()
        {
            try
            {
                var url = "Thanh toán thành công";
                return Ok(new { paymentUrl = url });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
