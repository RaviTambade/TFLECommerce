using ECommerceApplication.Models;
using ECommerceApplication.Repository.Interfaces;
using ECommerceApplication.Services.Interfaces;

namespace ECommerceApplication.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly IWishlistRepository _wishlistRepository;

        public WishlistService(IWishlistRepository wishlistRepository)
        {
            _wishlistRepository = wishlistRepository;
        }

        public bool AddToWishlist(int userId, int productId)
        {
            return _wishlistRepository.AddToWishlist(userId, productId);
        }

        public bool RemoveFromWishlist(int userId, int productId)
        {
            return _wishlistRepository.RemoveFromWishlist(userId, productId);
        }

        public List<WishlistItem> GetWishlist(int userId)
        {
            return _wishlistRepository.GetWishlist(userId);
        }
    }
}
