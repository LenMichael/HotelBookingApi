using Microsoft.EntityFrameworkCore;
using HotelBookingApi.Models;



namespace HotelBookingApi.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<HotelBooking> Bookings { get; set; }
        public DbSet<User> Users { get; set; }
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }
        public virtual DbSet<HotelBooking> GetBookings()
        {
            return Bookings;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HotelBooking>().HasData(
                new HotelBooking { Id = 1, RoomNumber = 101, ClientName = "John Doe" },
                new HotelBooking { Id = 2, RoomNumber = 102, ClientName = "Jane Smith" }
            );

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "admin", PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123") }
            );
        }

    }
}
