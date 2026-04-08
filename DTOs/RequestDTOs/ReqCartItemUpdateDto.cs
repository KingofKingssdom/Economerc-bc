namespace Ecommerce.DTOs.RequestDTOs
{
    public class ReqCartItemUpdateDto
    {
        public long CartItemId { get; set; }
        public string ProductName { get; set; } 
        public string ChangeType { get; set; } 
        public string Message { get; set; }    
        public double? OldPrice { get; set; }
        public double? NewPrice { get; set; }
        public int? NewStock { get; set; }
    }
}
