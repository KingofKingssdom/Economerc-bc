using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost]
        public async Task<ActionResult<ResProductDto>> Create(ReqProductDto reqProductDto)
        {
            var product = await _productService.CreateProduct(reqProductDto);
            return Ok(new
            {
                message = "Created success!",
                data = product
            });
        }

        [HttpGet]
        public async Task<ActionResult<ResBrandDto>> GetAll()
        {
            var products = await _productService.GetAllProduct();
            if (products == null)
            {
                return NotFound(new { message = "Products not found!" });
            }

            return Ok(new
            {
                message = "Get Product success",
                data = products
            });

        }
        [HttpPut("{productCode}")]
        public async Task<ActionResult<ResProductDto>> Update(string productCode, ReqProductDto reqProductDto)
        {
            var product = await _productService.UpdateProduct(productCode, reqProductDto);
            try
            {
                return Ok(new
                {
                    message = "Update success",
                    data = product
                });
            }catch(Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
        [HttpDelete("{productCode}")]
        public async Task<ActionResult<ResProductDto>> Delete(string productCode)
        {
            var product = await _productService.DeleteProduct(productCode);
            try
            {
                return Ok(new
                {
                    message = "Delete success",
                    data = product
                });
            }catch(Exception ex)
            {
                return NotFound(new { ex.Message });
            }
        }

    }
}
