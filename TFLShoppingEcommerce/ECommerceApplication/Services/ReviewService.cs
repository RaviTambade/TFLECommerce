using ECommerceApplication.Models;
using ECommerceApplication.Repository.Interfaces;
using ECommerceApplication.Services.Interfaces;

namespace ECommerceApplication.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public bool AddReview(int productId, int userId, int rating, string reviewText)
        {
            return _reviewRepository.AddReview(productId, userId, rating, reviewText);
        }

        public List<Review> GetReviews(int productId)
        {
            return _reviewRepository.GetReviews(productId);
        }

        public decimal GetAverageRating(int productId)
        {
            return _reviewRepository.GetAverageRating(productId);
        }

        public bool HasUserPurchasedProduct(int userId, int productId)
        {
            return _reviewRepository.HasUserPurchasedProduct(userId, productId);
        }
    }
}
