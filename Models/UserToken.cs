namespace Ecommerce.Models
{
    public class UserToken
    {
        public long Id { set; get; }
        public long UserId { set; get; }
        public User User { set; get; }
        public string Token { set; get; }
        public bool IsUsed { set; get; }
        public bool IsRevoked { set; get; }
        public DateTime AddedDate { set; get; }
        public DateTime ExpiryDate { set; get; }
        public string Jti { set; get; }
    }
}
