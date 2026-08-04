using ECommerceApplication.Models;

namespace ECommerceApplication.Repository.Interfaces
{
    public interface IReviewRepository
    {
        bool AddReview(int productId, int userId, int rating, string reviewText);
        List<Review> GetReviews(int productId);
        decimal GetAverageRating(int productId);
        bool HasUserPurchasedProduct(int userId, int productId);
    }
}
