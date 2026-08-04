using ECommerceApplication.Models;
using ECommerceApplication.Repository.Interfaces;
using ECommerceApplication.Services.Interfaces;

namespace ECommerceApplication.Services
{
    public class CouponService : ICouponService
    {
        private readonly ICouponRepository _couponRepository;

        public CouponService(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        public Task<CouponApplyResult> applyCouponAsync(string code, decimal totalAmount)
        {
            return _couponRepository.applyCouponAsync(code, totalAmount);
        }
    }
}
