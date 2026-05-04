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
        [HttpGet("{productCode}")]
        public async Task<ActionResult<ResProductDto>> GetByProductCode(string productCode)
        {
            try {
                var product = await _productService.GetByProductCode(productCode);
                return Ok(new
                {
                    message = "Get success!",
                    data = product
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ResProductDto>> Update(long id, ReqProductDto reqProductDto)
        {
            var product = await _productService.UpdateProduct(id, reqProductDto);
            try
            {
                return Ok(new
                {
                    message = "Update success",
                    data = product
                });
            } catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResProductDto>> Delete(long id)
        {
            var product = await _productService.DeleteProduct(id);
            try
            {
                return Ok(new
                {
                    message = "Delete success",
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
            var product = _productService.GetAllProductByIsFeatured(categoryId);
            return Ok(new
            {
                message = "get product by fetured success!",
                data = product
            });
        }
        [HttpGet("IsOnPromotion")]
        public async Task<ActionResult> GetByIsOnPromotion()
        {
            var product = _productService.GetAllProductByIsOnPromotion();
            return Ok(new
            {
                message = "get product by promotion success!",
                data = product
            });
        }
        [HttpGet("productId/{id}")]
        public async Task<ActionResult> GetProductById(long id)
        {
            var product = _productService.GetProductById(id);
            return Ok(new
            {
                message = "Get product success !",
                data = product
            });
        }
        [HttpGet("productName/{productName}")]
        public async Task<ActionResult<ResBrandDto>> GetAllByProductName(string productName)
        {
            var products = await _productService.GetAllByProductByName(productName);
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
    }
}
