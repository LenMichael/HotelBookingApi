using System.ComponentModel.DataAnnotations;

namespace HotelBookingApi.Models
{
    public class Inventory
    {
        public int Id { get; set; } // Primary Key

        [Required]
        [StringLength(100, ErrorMessage = "Item name cannot be longer than 100 characters.")]
        public string Name { get; set; } // Όνομα αντικειμένου (π.χ., Towels, Shampoo)

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative value.")]
        public int Quantity { get; set; } // Ποσότητα διαθέσιμη

        [Required]
        public DateTime LastUpdated { get; set; } // Ημερομηνία τελευταίας ενημέρωσης
    }
}
