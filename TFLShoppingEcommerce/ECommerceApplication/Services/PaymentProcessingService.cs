using ECommerceApplication.Models;
using ECommerceApplication.Repository.Interfaces;
using ECommerceApplication.Services.Interfaces;

namespace ECommerceApplication.Services
{
    public class PaymentProcessingService : IPaymentProcessingService
    {
        private readonly IPaymentProcessingRepository _paymentRepo;

        public PaymentProcessingService(IPaymentProcessingRepository repo)
        {
            _paymentRepo = repo;
        }

        public bool SavePayment(int orderId, DateTime paymentDate, int paymentAmount, string paymentMethod, string paymentStatus)
        {
            return _paymentRepo.SavePayment(orderId, paymentDate, paymentAmount, paymentMethod, paymentStatus);
        }


    }
}