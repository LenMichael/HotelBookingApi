using System.ComponentModel.DataAnnotations;

namespace HotelBookingApi.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public required string Username { get; set; }
        [Required]
        public required string PasswordHash { get; set; }
        [Required]
        [RegularExpression("^(Admin|Customer|Employee)$", ErrorMessage = "Role must be either 'Admin', 'Customer', or 'Employee'.")]
        public required string Role { get; set; }
    }

}
