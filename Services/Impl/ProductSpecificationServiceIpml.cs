using Ecommerce.Data;
using Ecommerce.DTOs.RequestDTOs;
using Ecommerce.DTOs.ResponseDTOs;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services.Impl
{
    public class ProductSpecificationServiceIpml : IProductSpecificationService
    {
        private readonly MyAppContext _context;
        public ProductSpecificationServiceIpml(MyAppContext context)
        {
            _context = context;
        }

        public async Task<ResProductSpecificationDto>
                CreateProductSpecification(ReqProductSpecificationDto reqProductSpecificationDto)
        {
            ProductSpecification productSpecification = new ProductSpecification()
            {
                SpecificationName = reqProductSpecificationDto.SpecificationName
            };
            await _context.ProductSpecifications.AddAsync(productSpecification);
            await _context.SaveChangesAsync();
            return new ResProductSpecificationDto
            {
                Id = productSpecification.Id,
                SpecificationName = productSpecification.SpecificationName
            };
        }
        public async Task<ResProductSpecificationDto> GetProductSpecificationById(long id)
        {
            ProductSpecification productSpecification = await _context.ProductSpecifications
                        .FirstOrDefaultAsync(ps => ps.Id == id);
            if (productSpecification == null)
            {
                throw new Exception($"Product specification with Id ={id} not found");
            }
            return new ResProductSpecificationDto()
            {
                Id = productSpecification.Id,
                SpecificationName = productSpecification.SpecificationName
            };
        }
        public async Task<List<ResProductSpecificationDto>> GetAllProductSpecification()
        {
            List<ProductSpecification> productSpecifications = await _context.ProductSpecifications
                    .ToListAsync();
            var resProductSpecification = productSpecifications.
                Select(ps => new ResProductSpecificationDto
                {
                    Id = ps.Id,
                    SpecificationName = ps.SpecificationName
                }).ToList();
            return resProductSpecification;
        }
        public async Task<ResProductSpecificationDto> UpdateProductSpecification(long id, ReqProductSpecificationDto reqProductSpecificationDto)
        {
            ProductSpecification productSpecification = await _context.ProductSpecifications
                        .FirstOrDefaultAsync(ps => ps.Id == id);
            if (productSpecification == null)
            {
                throw new Exception($"Product specification with Id ={id} not found");
            }
            productSpecification.SpecificationName = reqProductSpecificationDto.SpecificationName;
            await _context.SaveChangesAsync();
            return new ResProductSpecificationDto
            {
                Id = productSpecification.Id,
                SpecificationName = productSpecification.SpecificationName
            };
        }
        public async Task<ResProductSpecificationDto> DeleteProductSpecification(long id)
        {
            ProductSpecification productSpecification = await _context.ProductSpecifications
                        .FirstOrDefaultAsync(ps => ps.Id == id);
            if (productSpecification == null)
            {
                throw new Exception($"Product specification with Id ={id} not found");
            }
            _context.ProductSpecifications.Remove(productSpecification);
            return new ResProductSpecificationDto
            {
                Id = productSpecification.Id,
                SpecificationName = productSpecification.SpecificationName
            };
        }
    }
}
