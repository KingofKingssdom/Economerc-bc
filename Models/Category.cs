using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Models
{
    [Index(nameof(CategoryCode), IsUnique =true)]
    public class Category
    {
        public long Id { set; get; }
        public string CategoryCode { set; get; }
        public string CategoryName { set; get; }
        public List<CategoryBrand> CategoryBrand { get; } = [];
        public List<Product> Products { get; } = new List<Product>();

    }
}
