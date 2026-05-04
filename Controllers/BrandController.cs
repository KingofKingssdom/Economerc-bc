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
                var resBrandDto = await _brandService.CreateBrandAsync(reqBrandDto);
                return Ok(new
                {
                    message = "Created success",
                    data = reqBrandDto
                });
            }catch(Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
        [HttpGet]
        public async Task<ActionResult<ResBrandDto>> GetAll()
        {
            var resBrandDtos = await _brandService.GetAllBrandAsync();
            if(resBrandDtos == null)
            {
                return NotFound(new { message = "Brands not found!" });
            }
            return Ok(new
            {
                message = "Get Brands success",
                data = resBrandDtos
            });
        }
        [HttpGet("{brandCode}")]
        public async Task<ActionResult<ResBrandDto>> GetByBrandCode(string brandCode)
        {
            try
            {
                var resBrandDto = await _brandService.GetBrandByBrandCodeAsync(brandCode);
                return Ok(new
                {
                    message= "Get data successfully",
                    data = resBrandDto
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
                    message = "Get data successfully",
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
                var resBrandDto = await _brandService.UpdateBrandAsync(id,reqBrandDto);
                return Ok(new
                {
                    message = "Update data successfully",
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
                var resBrandDto = await _brandService.DeleteBrandAsync(id);
                return Ok(new
                {
                    message = "Delete success",
                    data = resBrandDto
                });
            }catch(Exception ex)
            {
              return  NotFound(new { message = ex.Message });
            }

        }
       
    }
}
