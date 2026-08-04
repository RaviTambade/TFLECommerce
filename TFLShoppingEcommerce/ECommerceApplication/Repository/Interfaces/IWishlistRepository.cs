using ECommerceApplication.Models;

namespace ECommerceApplication.Repository.Interfaces
{
    public interface IWishlistRepository
    {
        bool AddToWishlist(int userId, int productId);
        bool RemoveFromWishlist(int userId, int productId);
        List<WishlistItem> GetWishlist(int userId);
    }
}
