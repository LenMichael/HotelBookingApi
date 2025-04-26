using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingApi.Models
{
    public class Room
    {
        // We added fluent validation for this model, so we can remove the Data Annotations

        public int Id { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Room number must be greater than 0.")]
        public int RoomNumber { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Room type cannot be longer than 50 characters.")]
        public string Type { get; set; } 

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal Price { get; set; }

        [Required]
        public int HotelId { get; set; } // Foreign Key

        [ForeignKey("HotelId")]
        public Hotel Hotel { get; set; } // Navigation Property
    }
}
