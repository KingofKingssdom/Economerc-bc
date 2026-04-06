namespace Ecommerce.DTOs.ResponseDTOs
{
    public class ResUserDto
    {
        public long Id { set; get; }
        public string FullName { set; get; }

        public string PhoneNumber { set; get; }
        public string Email { set; get; }
        public List<ResRoleDto> ResRoleDtos { set; get; }

    }
}
