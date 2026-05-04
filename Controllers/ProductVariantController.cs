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

        [HttpPost]
        public async Task<ActionResult<ResProductVariantDto>> Create(ReqProductVariantDto reqProductVariantDto)
        {
            var productVariant = await _productVariantService.CreateProductVariant(reqProductVariantDto);
            return Ok(new
            {
                message = "Create success",
                data = productVariant
            });
        }
        [HttpGet]
        public async Task<ActionResult<ResProductVariantDto>> GetAll()
        {
            var productVariant = await _productVariantService.GetAllProductVariant();
            return Ok(new
            {
                message = "Get all product color success",
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
                    message = "Get success",
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
                    message = "Update success",
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
                    message = "Delete success",
                    data = productVariant
                });
            }
            catch (Exception ex)
            {
                return NotFound(new { ex.Message });
            }

        }
    }
}
