using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/brand")]
    //[Authorize(Roles ="Admin")]
    public class BrandController: ControllerBase
    {
        private readonly IBrandService _brandService;
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpPost]
        public async Task<ActionResult<ResBrandDto>> Create(ReqBrandDto reqBrandDto)
        {
            try
            {
                var resBrand = await _brandService.CreateBrand(reqBrandDto);
                return Ok(new
                {
                    message = "Data is created successfully",
                    data = resBrand
                });
            }catch(Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
        [HttpGet]
        public async Task<ActionResult<ResBrandDto>> GetAll()
        {
            var resBrands = await _brandService.GetAllBrand();
            return Ok(new
            {
                message = "Data is retrieved successfully",
                data = resBrands
            });
        }
        [HttpGet("{brandCode}")]
        public async Task<ActionResult<ResBrandDto>> GetByBrandCode(string brandCode)
        {
            try
            {
                var resBrand = await _brandService.GetBrandByBrandCode(brandCode);
                return Ok(new
                {
                    message= "Data is retrieved successfully",
                    data = resBrand
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
            try
            {
                var resBrandDto = await _brandService.GetAllBrandByCategoryId(categoryId);
                return Ok(new
                {
                    message = "Data is retrieved successfully",
                    data = resBrandDto
                });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }


        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ResBrandDto>> Update(long id, ReqBrandDto reqBrandDto)
        {
            try
            {
                var resBrandDto = await _brandService.UpdateBrand(id,reqBrandDto);
                return Ok(new
                {
                    message = "Data is updated successfully",
                    data = reqBrandDto
                });
            }
            catch(Exception ex)
            {
               return NotFound(new { message = ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResBrandDto>> Delete(long id)
        {
            try
            {
                var resBrandDto = await _brandService.DeleteBrand(id);
                return Ok(new
                {
                    message = "data is deleted successfully",
                    data = resBrandDto
                });
            }catch(Exception ex)
            {
              return  NotFound(new { message = ex.Message });
            }

        }
       
    }
}
