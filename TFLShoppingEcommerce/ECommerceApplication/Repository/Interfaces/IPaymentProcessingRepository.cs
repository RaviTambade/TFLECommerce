using ECommerceApplication.Models;

namespace ECommerceApplication.Repository.Interfaces
{
    public interface IPaymentProcessingRepository
    {
        bool SavePayment(int orderId, DateTime paymentDate, int paymentAmount, string paymentMethod, string paymentStatus);
    }
}