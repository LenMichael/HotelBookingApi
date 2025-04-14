using System.ComponentModel.DataAnnotations;

namespace HotelBookingApi.Models
{
    public class Log
    {
        public int Id { get; set; } // Primary Key

        [Required]
        public int UserId { get; set; } // Foreign Key Users

        [Required]
        [StringLength(200, ErrorMessage = "Action description cannot be longer than 200 characters.")]
        public string Action { get; set; } 

        [Required]
        public DateTime Timestamp { get; set; } 

        public User User { get; set; }
    }
}
