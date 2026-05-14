using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles ="Admin, Customer")]
        public async Task<ActionResult<ResProductDto>> Create(ReqProductDto reqProductDto)
        {
            var product = await _productService.CreateProduct(reqProductDto);
            return Ok(new
            {
                message = "Data is created successfully!",
                data = product
            });
        }

        [HttpGet]
        public async Task<ActionResult<ResBrandDto>> GetAll()
        {
            var products = await _productService.GetAllProduct();

            return Ok(new
            {
                message = "Data is retrieved successfully",
                data = products
            });

        }
        [HttpGet("{productCode}")]
        public async Task<ActionResult<ResProductDto>> GetByProductCode(string productCode)
        {
            try {
                var product = await _productService.GetByProductCode(productCode);
                return Ok(new
                {
                    message = "Data is retrieved successfully!",
                    data = product
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResProductDto>> Update(long id, ReqProductDto reqProductDto)
        {
            
            try   
            {
                var product = await _productService.UpdateProduct(id, reqProductDto);
                return Ok(new
                {
                    message = "Data is updated successfully",
                    data = product
                });
            } catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<ResProductDto>> Delete(long id)
        {
            try
            {
                var product = await _productService.DeleteProduct(id);
                return Ok(new
                {
                    message = "Data is deleted successfully",
                    data = product
                });
            } catch (Exception ex)
            {
                return NotFound(new { ex.Message });
            }
        }
        [HttpGet("IsFeatured/{categoryId}")]
        public async Task<ActionResult> GetByIsFeatured(long categoryId)
        {
            try
            {
                var product = _productService.GetAllProductByIsFeatured(categoryId);
                return Ok(new
                {
                    message = "Data is retrieved successfully!",
                    data = product
                });
            }
            catch(Exception ex){
                return BadRequest(new { message = ex.Message });
            }
            
        }
        [HttpGet("IsOnPromotion")]
        public async Task<ActionResult> GetByIsOnPromotion()
        {
            var product = _productService.GetAllProductByIsOnPromotion();
            return Ok(new
            {
                message = "Data is retrieved successfully!",
                data = product
            });
        }
        [HttpGet("productId/{id}")]
        public async Task<ActionResult> GetProductById(long id)
        {
            try {
                var product = _productService.GetProductById(id);
                return Ok(new
                {
                    message = "Data is retrieved successfully !",
                    data = product
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            
        }
        [HttpGet("productName/{productName}")]
        public async Task<ActionResult<ResBrandDto>> GetAllByProductName(string productName)
        {
            try
            {
                var products = await _productService.GetAllByProductByName(productName);
                return Ok(new
                {
                    message = "Data is retrieved successfully",
                    data = products
                });
            }
            catch(Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }

        }
        [HttpGet("categoryId/{categoryId}")]
        public async Task<ActionResult<ResBrandDto>> GetAllByCategoryId(long categoryId)
        {
            var products = await _productService.GetAllProductByCategoryId(categoryId);
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
        [HttpGet("categoryId/{categoryId}/brandId/{brandId}")]
        public async Task<ActionResult<ResBrandDto>> GetAllByCategoryId(long categoryId, long brandId)
        {
            var products = await _productService.GetAllProductByCategoryIdAndBrandId(categoryId, brandId);
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
    }
}
