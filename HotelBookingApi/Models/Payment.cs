using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingApi.Models
{
    public class Payment
    {
        public int Id { get; set; }

        [Required]
        public int BookingId { get; set; } // Foreign Key

        [ForeignKey("BookingId")]
        public Booking Booking { get; set; } // Navigation Property

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a positive value.")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Payment method cannot be longer than 50 characters.")]
        public string PaymentMethod { get; set; } // π.χ., Credit Card, Cash
    }
}
