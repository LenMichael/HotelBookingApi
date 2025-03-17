using System.ComponentModel.DataAnnotations;

namespace HotelBookingApi.Models
{
    public class HotelBooking
    {
        public int Id { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Room number must be greater than 0.")]
        public int RoomNumber { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Client name cannot be longer than 100 characters.")]
        public string? ClientName { get; set; } 
    }
}
