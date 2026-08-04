using System.ComponentModel.DataAnnotations;

namespace ECommerceApplication.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        public string ReviewText { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
        public string UserName { get; set; } = string.Empty;
    }
}
