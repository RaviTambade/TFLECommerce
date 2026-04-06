using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApplication.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string? OrderId { get; set; }

        public string? PaymentId { get; set; }
        public string? Status { get; set; }

        public decimal? Amount { get; set; }
    }
}