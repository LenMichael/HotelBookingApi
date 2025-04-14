using System.ComponentModel.DataAnnotations;

namespace HotelBookingApi.Models
{
    public class Shift
    {
        public int Id { get; set; } // Primary Key

        [Required]
        public int EmployeeId { get; set; } // Foreign Key Employees

        [Required]
        public DateTime StartTime { get; set; } 

        [Required]
        public DateTime EndTime { get; set; } 

        [Required]
        [StringLength(50, ErrorMessage = "Role cannot be longer than 50 characters.")]
        public string Role { get; set; } 
        
        public Employee Employee { get; set; }
    }
}
