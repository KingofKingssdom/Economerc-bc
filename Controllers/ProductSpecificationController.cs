using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/productSpecification")]
    public class ProductSpecificationController: ControllerBase
    {
        private readonly IProductSpecificationService _productSpecificationService;
        public ProductSpecificationController(IProductSpecificationService productSpecificationService)
        {
            _productSpecificationService = productSpecificationService;
        }
        [HttpPost]
        public async Task<ActionResult<ResProductSpecificationDto>> Create(ReqProductSpecificationDto reqProductSpecificationDto)
        {
            var productSpecification = await _productSpecificationService.CreateProductSpecification(reqProductSpecificationDto);
            return Ok(new
            {
                message = "Create success",
                data = productSpecification
            });
        }
        [HttpGet]
        public async Task<ActionResult<ResProductSpecificationDto>> GetAll()
        {
            var productSpecification = await _productSpecificationService.GetAllProductSpecification();
            return Ok(new
            {
                message = "Get all product specification success",
                data = productSpecification
            });
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ResProductSpecificationDto>> GetById(long id)
        {
            try
            {
                var productSpecification = await _productSpecificationService.GetProductSpecificationById(id);
                return Ok(new
                {
                    message = "Get success",
                    data = productSpecification
                });
            }
            catch (Exception ex)
            {
                return NotFound(new { ex.Message });
            }


        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ResProductSpecificationDto>> Update(long id, ReqProductSpecificationDto reqProductSpecification)
        {
            try
            {
                var productSpecification = await _productSpecificationService.UpdateProductSpecification(id, reqProductSpecification);
                return Ok(new
                {
                    message = "Update success",
                    data = productSpecification
                });
            }
            catch (Exception ex)
            {
                return NotFound(new { ex.Message });
            }

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResProductSpecificationDto>> Delete(long id)
        {
            try
            {
                var productSpecification = await _productSpecificationService.DeleteProductSpecification(id);
                return Ok(new
                {
                    message = "Delete success",
                    data = productSpecification
                });
            }
            catch (Exception ex)
            {
                return NotFound(new { ex.Message });
            }

        }
    }
}
