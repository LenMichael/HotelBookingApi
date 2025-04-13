using System.ComponentModel.DataAnnotations;

namespace HotelBookingApi.Models
{
    public class Hotel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Hotel name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Address cannot be longer than 200 characters.")]
        public string Address { get; set; }

        [StringLength(15, ErrorMessage = "Phone number cannot be longer than 15 characters.")]
        public string? PhoneNumber { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
        public ICollection<Room> Rooms { get; set; } = new List<Room>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
