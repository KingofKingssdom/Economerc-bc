namespace Ecommerce.Models
{
    public class Role
    {
        public long Id { set; get; }
        public string RoleName { set; get; }
        public List<UserRole> UserRoles { get; } = [];
    }
}
