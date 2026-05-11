using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers

{
    [ApiController]
    [Route("api/product-variants")]
    public class ProductVariantController: ControllerBase
    {
        private readonly IProductVariantService _productVariantService;
        public ProductVariantController(IProductVariantService productVariantService)
        {
            _productVariantService = productVariantService;
        }

        [HttpPost("productId/{productId}")]
        public async Task<ActionResult<ResProductVariantDto>> Create(ReqProductVariantDto reqProductVariantDto, long productId)
        {
            var productVariant = await _productVariantService.CreateProductVariant(reqProductVariantDto, productId);
            return Ok(new
            {
                message = "Data is created successfully",
                data = productVariant
            });
        }
        [HttpGet]
        public async Task<ActionResult<ResProductVariantDto>> GetAll()
        {
            var productVariant = await _productVariantService.GetAllProductVariant();
            return Ok(new
            {
                message = "Data is retrieved successfully",
                data = productVariant
            });
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ResProductVariantDto>> GetById(long id)
        {
            try
            {
                var productVariant = await _productVariantService.GetProductVariantById(id);
                return Ok(new
                {
                    message = "Data is retrieved successfully",
                    data = productVariant
                });
            }
            catch (Exception ex)
            {
                return NotFound(new { ex.Message });
            }


        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ResProductVariantDto>> Update(long id, ReqProductVariantDto reqProductVariant)
        {
            try
            {
                var productVariant = await _productVariantService.UpdateProductVariant(id, reqProductVariant);
                return Ok(new
                {
                    message = "Data is updated successfully",
                    data = productVariant
                });
            }
            catch (Exception ex)
            {
                return NotFound(new { ex.Message });
            }

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResProductVariantDto>> Delete(long id)
        {
            try
            {
                var productVariant = await _productVariantService.DeleteProductVariant(id);
                return Ok(new
                {
                    message = "Data is deleted successfully",
                    data = productVariant
                });
            }
            catch (Exception ex)
            {
                return NotFound(new { ex.Message });
            }

        }
        [HttpGet("productId/{productId}")]
        public async Task<ActionResult> GetAllByProductId(long productId)
        {
            var respoductVariant = _productVariantService.GetAllProductVariantByProductId(productId);
            return Ok(new
            {
                message = "Data is retrieved successfully",
                data = respoductVariant
            });
        }
    }
}
