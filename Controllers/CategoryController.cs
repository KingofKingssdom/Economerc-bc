using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController (ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpPost]
        public async Task<ActionResult<ResCategoryDto>> Create(ReqCategoryDto reqCatgoryDto)
        {
            try
            {
                var resCategory = await _categoryService.CreateCategory(reqCatgoryDto);
                return Ok(
               new
               {
                   message = "Data is created successfully!",
                   data = resCategory
               });
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            
        }
        [HttpGet]
        public async Task<ActionResult<ResCategoryDto>> GetAll()
        {
            var resCategories = await _categoryService.GetAllCategory();
            return Ok(new
            {
                message = "Data is retrieved successfully",
                data = resCategories
            });

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResCategoryDto>> GetById(long id)
        {
            try {
                ResCategoryDto resCategoryDto = await _categoryService.GetCategoryById(id);
                return Ok(new
                {
                    message = "Data is retrieved successfully",
                    data = resCategoryDto
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("categoryId/{categoryId}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResCategoryDto>> Update(long categoryId, ReqCategoryDto reqCategoryDto)
        {
            try
            {
                var resCategory = await _categoryService.UpdateCategory(categoryId, reqCategoryDto);
                return Ok(new
                {
                    message = "Data is updated successfully",
                    data = resCategory
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
           
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResCategoryDto>> Delete(long id)
        {
            try
            {
               var dataDeleted = await _categoryService.DeleteCategory(id);
                return Ok(new {
                        message = "Data is deleted successfully",
                        data = dataDeleted
                    });
            }
            catch(Exception ex)
            {
                return NotFound(new { ex.Message });
            }
            
        }
        [HttpGet("categoryCode/{categoryCode}")]
        public async Task<ActionResult<ResCategoryDto>> GetByCategoryCode(string categoryCode)
        {
            try
            {
                var category = await _categoryService.GetCategoryByCategoryCode(categoryCode);
                return Ok(new
                {
                    message = "Data is retrieved successfully",
                    data = category
                });
            }
            catch (Exception ex)
            {
                return NotFound(new { ex.Message });
            }

        }
    }
}
