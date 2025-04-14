using System.ComponentModel.DataAnnotations;

namespace HotelBookingApi.Models
{
    public class Feedback
    {
        public int Id { get; set; } // Primary Key

        [Required]
        public int EmployeeId { get; set; } // Foreign Key Employees

        [Required]
        [StringLength(500, ErrorMessage = "Message cannot be longer than 500 characters.")]
        public string Message { get; set; } 

        [Required]
        public DateTime CreatedAt { get; set; } 

        public Employee Employee { get; set; }
    }
}
