using ECommerceApplication.Models;

namespace ECommerceApplication.Repository.Interfaces
{
    public interface ICouponRepository
    {
        Task<CouponApplyResult> applyCouponAsync(string code, decimal totalAmount);
    }
}
