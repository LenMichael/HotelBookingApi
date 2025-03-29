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
    }

}
