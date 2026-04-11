namespace Ecommerce.Models
{
    public class User
    {
        public long Id { set; get; }
        public string FullName { set; get; }
        public string PhoneNumber { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
        public List<UserRole> UserRoles { get; } = [];
        public List<Order> Orders { get; } = new List<Order>();
    }
}
