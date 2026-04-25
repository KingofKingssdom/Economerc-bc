using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/specificationDetail")]
    public class SpecificationDetailController : ControllerBase
    {
        private readonly ISpecificationDetailService _specificationDetailService;
        public SpecificationDetailController(ISpecificationDetailService specificationService)
        {
            _specificationDetailService = specificationService;
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResSpecificationDetailDto>> Create(List<ReqSpecificationDetailDto> reqSpecificationDetailDto)
        {
            var specificationDetail = await _specificationDetailService.CreateSpecificationDetail(reqSpecificationDetailDto);
            return Ok(new
            {
                message = "Create success",
                data = specificationDetail
            });
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<ActionResult<ResSpecificationDetailDto>> GetAll()
        {
            var specificationDetail = await _specificationDetailService.GetAllSpecificationDetail();
            return Ok(new
            {
                message = "Get all specification detail success",
                data = specificationDetail
            });
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ResSpecificationDetailDto>> GetById(long id)
        {
            try
            {
                var specificationDetail = await _specificationDetailService.GetSpecificationDetailById(id);
                return Ok(new
                {
                    message = "Get success",
                    data = specificationDetail
                });
            }
            catch (Exception ex)
            {
                return NotFound(new { ex.Message });
            }


        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResSpecificationDetailDto>> Update(long productId,long productSpecificationId, ReqSpecificationDetailDto reqSpecificationDetail)
        {
            try
            {
                var specificationeDetail = await _specificationDetailService.UpdateSpecificationDetail(productId, productSpecificationId, reqSpecificationDetail);
                return Ok(new
                {
                    message = "Update success",
                    data = specificationeDetail
                });
            }
            catch (Exception ex)
            {
                return NotFound(new { ex.Message });
            }

        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResSpecificationDetailDto>> Delete(long id)
        {
            try
            {
                var specificationDetail = await _specificationDetailService.DeleteSpecificationDetail(id);
                return Ok(new
                {
                    message = "Delete success",
                    data = specificationDetail
                });
            }
            catch (Exception ex)
            {
                return NotFound(new { ex.Message });
            }

        }
    }
}
