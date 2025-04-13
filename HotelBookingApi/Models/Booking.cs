using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingApi.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public int RoomId { get; set; } // Foreign Key

        [ForeignKey("RoomId")]
        public Room Room { get; set; } // Navigation Property

        [Required]
        public int UserId { get; set; } // Foreign Key

        [ForeignKey("UserId")]
        public User User { get; set; } // Navigation Property

        [Required]
        public DateTime CheckInDate { get; set; }

        [Required]
        public DateTime CheckOutDate { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Status cannot be longer than 50 characters.")]
        public string Status { get; set; } // π.χ., Confirmed, Cancelled
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
