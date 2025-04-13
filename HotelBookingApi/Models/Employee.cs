using System.ComponentModel.DataAnnotations;

namespace HotelBookingApi.Models
{
    public class Employee
    {
        public int Id { get; set; } // Primary Key

        [Required]
        public int UserId { get; set; } // Foreign Key Users

        [Required]
        public int HotelId { get; set; } // Foreign Key Hotels

        [Required]
        [RegularExpression("^(Manager|Receptionist|Housekeeping|Chef)$", ErrorMessage = "Position must be 'Manager', 'Receptionist', 'Housekeeping', or 'Chef'.")]
        public string Position { get; set; } // Position (e.g., Manager, Receptionist)

        public User User { get; set; }
        public Hotel Hotel { get; set; }
    }
}
