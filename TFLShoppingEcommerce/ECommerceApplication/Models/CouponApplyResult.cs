namespace ECommerceApplication.Models
{
    public class CouponApplyResult
    {
        public bool Success { get; set; }
        public decimal Discount { get; set; }
        public decimal FinalAmount { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? Code { get; set; }
    }
}
