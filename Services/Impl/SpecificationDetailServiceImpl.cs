using Ecommerce.Data;
using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services.Impl
{
    public class SpecificationDetailServiceImpl: ISpecificationDetailService
    {
        private readonly MyAppContext _context;
        public SpecificationDetailServiceImpl(MyAppContext context)
        {
            _context = context;
        }
        public async Task<List<ResSpecificationDetailDto>> CreateSpecificationDetail(List<ReqSpecificationDetailDto> reqSpecificationDetailDto)
        {
            var reqSpecificationDetail = reqSpecificationDetailDto.Select(sd => new SpecificationDetail()
            {
                LableSpecification = sd.LableSpecification,
                ValueSpecification = sd.ValueSpecification,
                ProductId = sd.ProductId,
                ProductSpecificationId = sd.ProductSpecificationId
            }).ToList();
      
           
            await _context.SpecificationDetail.AddRangeAsync(reqSpecificationDetail);
            await _context.SaveChangesAsync();
            
            var result = reqSpecificationDetail.Select(sd => new ResSpecificationDetailDto
            {
                LableSpecification = sd.LableSpecification,
                ValueSpecification = sd.ValueSpecification,
                ProductId = sd.ProductId,
                ProductSpecificationId = sd.ProductSpecificationId
            }).ToList();

            return result;


        }
        public async Task<ResSpecificationDetailDto> GetSpecificationDetailById(long id)
        {
            SpecificationDetail specificationDetail =await _context.SpecificationDetail
                .FirstOrDefaultAsync(pd => pd.Id == id);
            if(specificationDetail == null)
            {
                throw new Exception($"Specification detail with Id={id} not found");
            }
            return new ResSpecificationDetailDto()
            {
                LableSpecification = specificationDetail.LableSpecification,
                ValueSpecification = specificationDetail.ValueSpecification,
                ProductId = specificationDetail.ProductId,
                ProductSpecificationId = specificationDetail.ProductSpecificationId
            };
        }
        public async Task<List<ResSpecificationDetailDto>> GetAllSpecificationDetail()
        {
            List<SpecificationDetail> specificationDetails =
                _context.SpecificationDetail.ToList();
            var result = specificationDetails.Select(sd => new ResSpecificationDetailDto
            {
                LableSpecification = sd.LableSpecification,
                ValueSpecification = sd.ValueSpecification,
                ProductId = sd.ProductId,
                ProductSpecificationId = sd.ProductSpecificationId
            }).ToList();

            return result;
        }

        public async Task<List<ResSpecificationDetailDto>> UpdateSpecificationDetail(long productId, long productSpecificationId, ReqSpecificationDetailDto reqSpecificationDetailDto)
        {
            var result = new List<ResSpecificationDetailDto>();
            foreach(var item in result)
            {
                var entity = await _context.SpecificationDetail
            .FirstOrDefaultAsync(sd => sd.ProductId == productId &&
                                       sd.ProductSpecificationId == productSpecificationId);
                if (entity == null)
                {
                    throw new Exception($"Specification detail with produc Id={productId} and productSpecification Id={productSpecificationId} not found");
                }
                entity.LableSpecification = reqSpecificationDetailDto.LableSpecification;
                entity.ValueSpecification = reqSpecificationDetailDto.ValueSpecification;
                result.Add(new ResSpecificationDetailDto
                {
                    LableSpecification = entity.LableSpecification,
                    ValueSpecification = entity.ValueSpecification,
                    ProductId = entity.ProductId,
                    ProductSpecificationId = entity.ProductSpecificationId
                });

            }
            await _context.SaveChangesAsync();
            return result;
        }
        public async Task<ResSpecificationDetailDto> DeleteSpecificationDetail(long id)
        {
            SpecificationDetail specificationDetail = await _context.SpecificationDetail
                .FirstOrDefaultAsync(sd => sd.Id == id);
            if (specificationDetail == null)
            {
                throw new Exception($"Specification detail with Id={id} not found");
            }
            _context.SpecificationDetail.Remove(specificationDetail);
            await _context.SaveChangesAsync();
            return new ResSpecificationDetailDto()
            {
                LableSpecification = specificationDetail.LableSpecification,
                ValueSpecification = specificationDetail.ValueSpecification,
                ProductId = specificationDetail.ProductId,
                ProductSpecificationId = specificationDetail.ProductSpecificationId
            };
        }
    }
}
