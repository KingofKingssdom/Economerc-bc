namespace Ecommerce.Models
{
    public class Brand
    {
        public long Id { set; get; }
        public string brandCode { set; get; }
        public string brandName { set; get; }
        public string UrlImageBrand { set; get; }
        public List<CategoryBrand> CategoryBrand { get; set; } = [];
        public List<Product> Products { get; } = new List<Product>();
    }
}
