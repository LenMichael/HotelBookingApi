using HotelBookingApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingApi.DTOs
{
    public class RoomDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Room number must be greater than 0.")]
        public int RoomNumber { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Room type cannot be longer than 50 characters.")]
        public string Type { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal Price { get; set; }
    }
}
