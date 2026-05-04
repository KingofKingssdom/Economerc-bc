using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController (ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResCategoryDto>> Create(ReqCategoryDto reqCatgoryDto)
        {
            try
            {
                var resCategory = await _categoryService.CreateCategoryAsync(reqCatgoryDto);
                return Ok(
               new
               {
                   message = "Created success!",
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
            var categories = await _categoryService.GetAllCategoryAsync();
            if(categories == null)
            {
                return NotFound(new { message = "Categories not found!" });
            }
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResCategoryDto>> GetById(long id)
        {
            ResCategoryDto? resCategoryDto = await _categoryService.GetCategoryByIdAsync(id);
            if(resCategoryDto == null)
            {
                return NotFound(new { message = $"Category with Id = {id} not found" });
            }
            return Ok(resCategoryDto);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResCategoryDto>> Update(long id, ReqCategoryDto reqCategoryDto)
        {
            try
            {
                ResCategoryDto? resCategoryDto = await _categoryService.UpdateCategoryAsync(id, reqCategoryDto);
                if (resCategoryDto == null)
                {
                    return NotFound(new { message = $"Category with Id = {id} not found" });
                }
                return Ok(resCategoryDto);
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
               var dataDeleted = await _categoryService.DeleteCategoryAsync(id);
                return Ok(
                    new
                    {
                        message = "Deleted success",
                        data = dataDeleted
                    }
                    );
            }
            catch(Exception ex)
            {
                return NotFound(new { ex.Message });
            }
            
        }
    }
}
