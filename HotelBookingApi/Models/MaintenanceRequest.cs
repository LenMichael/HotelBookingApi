using System.ComponentModel.DataAnnotations;

namespace HotelBookingApi.Models
{
    public class MaintenanceRequest
    {
        public int Id { get; set; } // Primary Key

        [Required]
        public int? RoomId { get; set; } // Foreign Key προς τον πίνακα Rooms (προαιρετικό)

        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; } // Περιγραφή του προβλήματος

        [Required]
        [StringLength(50, ErrorMessage = "Status cannot be longer than 50 characters.")]
        public string Status { get; set; } // Κατάσταση (π.χ., Pending, In Progress, Completed)

        [Required]
        public DateTime CreatedAt { get; set; } // Ημερομηνία δημιουργίας

        public DateTime? UpdatedAt { get; set; } // Ημερομηνία τελευταίας ενημέρωσης

        // Πλοήγηση
        public Room Room { get; set; }
    }
}
