using System.ComponentModel.DataAnnotations;

namespace HotelBookingApi.Models
{
    public class Review
    {
        public int Id { get; set; } // Primary Key

        [Required]
        public int HotelId { get; set; } // Foreign Key προς τον πίνακα Hotels

        [Required]
        public int UserId { get; set; } // Foreign Key προς τον πίνακα Users

        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; } // Βαθμολογία (1-5)

        [StringLength(500, ErrorMessage = "Comment cannot be longer than 500 characters.")]
        public string Comment { get; set; } // Σχόλιο

        // Πλοήγηση
        public Hotel Hotel { get; set; }
        public User User { get; set; }
    }
}
