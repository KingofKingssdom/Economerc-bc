namespace Ecommerce.DTOs.RequestDTOs
{
    public class ReqRefreshTokenDto
    {
        public string oldAccessToken { set; get; }
        public string oldRefreshToken { set; get; }
    }
}
