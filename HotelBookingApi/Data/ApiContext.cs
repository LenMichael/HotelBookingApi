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

        //Initial Migration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HotelBooking>().HasData(
                new HotelBooking { Id = 1, RoomNumber = 101, ClientName = "John Doe" },
                new HotelBooking { Id = 2, RoomNumber = 102, ClientName = "Jane Smith" },
                new HotelBooking { Id = 3, RoomNumber = 103, ClientName = "Alice Brown" }
            );

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "admin", PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"), Role = "Admin" },
                new User { Id = 2, Username = "user", PasswordHash = BCrypt.Net.BCrypt.HashPassword("user123"), Role = "User" }
            );
        }

    }
}
