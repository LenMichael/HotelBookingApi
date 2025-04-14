using System.ComponentModel.DataAnnotations;

namespace HotelBookingApi.Models
{
    public class Event
    {
        public int Id { get; set; } // Primary Key

        [Required]
        [StringLength(100, ErrorMessage = "Event name cannot be longer than 100 characters.")]
        public string Name { get; set; } 

        [Required]
        public DateTime Date { get; set; } 

        [Required]
        [StringLength(100, ErrorMessage = "Location cannot be longer than 100 characters.")]
        public string Location { get; set; } 

        [StringLength(200, ErrorMessage = "Organizer name cannot be longer than 200 characters.")]
        public string Organizer { get; set; } 

        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; } 
    }
}
