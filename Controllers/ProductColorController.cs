using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("/api/productColor")]
    public class ProductColorController:ControllerBase
    {
        private readonly IProductColorService _productColorService;
        public ProductColorController(IProductColorService productColorService)
        {
            _productColorService = productColorService;
        }

        [HttpPost]
        public async Task<ActionResult<ResProductColorDto>> Create(ReqProductColorDto reqProductColorDto)
        {
            var productColor = await _productColorService.CreateProductColor(reqProductColorDto);
            return Ok(new
            {
                message = "Create success",
                data = productColor
            });
        }
        [HttpGet]
        public async Task<ActionResult<ResProductColorDto>> GetAll()
        {
            var productColor = await _productColorService.GetAllProductColor();
            return Ok(new
            {
                message = "Get all product color success",
                data = productColor
            });
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ResProductColorDto>> GetById(long id)
        {
            try
            {
                var productColor = await _productColorService.GetProductColorById(id);
                return Ok(new
                {
                    message = "Get success",
                    data = productColor
                });
            }catch(Exception ex)
            {
                return NotFound(new { ex.Message });
            }
            
            
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ResProductColorDto>> Update(long id, ReqProductColorDto reqProductColor)
        {
            try
            {
                var productColor = await _productColorService.UpdateProductColor(id, reqProductColor);
                return Ok(new
                {
                    message = "Update success",
                    data = productColor
                });
            }catch(Exception ex)
            {
                return NotFound(new { ex.Message });
            }
           
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResProductColorDto>> Delete(long id)
        {
            try
            {
                var productColor = await _productColorService.DeleteProductColor(id);
                return Ok(new
                {
                    message = "Delete success",
                    data = productColor
                });
            }catch(Exception ex)
            {
                return NotFound(new { ex.Message });
            }

        }
    }
}
