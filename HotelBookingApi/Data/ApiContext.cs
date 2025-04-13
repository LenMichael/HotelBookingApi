using Microsoft.EntityFrameworkCore;
using HotelBookingApi.Models;

namespace HotelBookingApi.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }
        public virtual DbSet<Booking> GetBookings()
        {
            return Bookings;
        }

        //Initial Migration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "admin", PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"), Role = "Admin" },
                new User { Id = 2, Username = "customer", PasswordHash = BCrypt.Net.BCrypt.HashPassword("customer123"), Role = "Customer" },
                new User { Id = 3, Username = "employee", PasswordHash = BCrypt.Net.BCrypt.HashPassword("employee123"), Role = "Employee" }
            );


            modelBuilder.Entity<Hotel>().HasData(
                new Hotel { Id = 1, Name = "Grand Hotel", Address = "123 Main Street", PhoneNumber = "123-456-7890", Email = "info@grandhotel.com" },
                new Hotel { Id = 2, Name = "Ocean View", Address = "456 Beach Road", PhoneNumber = "987-654-3210", Email = "contact@oceanview.com" }
            );

            modelBuilder.Entity<Room>()
                .HasOne(r => r.Hotel)
                .WithMany(h => h.Rooms)
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, RoomNumber = 101, Type = "Single", Price = 50.00m, HotelId = 1 },
                new Room { Id = 2, RoomNumber = 102, Type = "Double", Price = 75.00m, HotelId = 1 },
                new Room { Id = 3, RoomNumber = 201, Type = "Suite", Price = 120.00m, HotelId = 2 }
            );

            //Rooms -> Bookings
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Room)
                .WithMany()
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            // Users -> Bookings
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany()
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Booking>().HasData(
                new Booking
                {
                    Id = 1,
                    RoomId = 1,
                    UserId = 3,
                    CheckInDate = DateTime.SpecifyKind(new DateTime(2025, 4, 20), DateTimeKind.Utc),
                    CheckOutDate = DateTime.SpecifyKind(new DateTime(2025, 4, 25), DateTimeKind.Utc),
                    Status = "Confirmed"
                },
                new Booking
                {
                    Id = 2,
                    RoomId = 2,
                    UserId = 3,
                    CheckInDate = DateTime.SpecifyKind(new DateTime(2025, 5, 1), DateTimeKind.Utc),
                    CheckOutDate = DateTime.SpecifyKind(new DateTime(2025, 5, 5), DateTimeKind.Utc),
                    Status = "Cancelled"
                }
            );

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Booking)
                .WithMany(b => b.Payments)
                .HasForeignKey(p => p.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Payment>().HasData(
                new Payment
                {
                    Id = 1,
                    BookingId = 1,
                    Amount = 250.00m,
                    PaymentDate = DateTime.SpecifyKind(new DateTime(2025, 4, 15), DateTimeKind.Utc),
                    PaymentMethod = "Credit Card"
                },
                new Payment
                {
                    Id = 2,
                    BookingId = 2,
                    Amount = 375.00m,
                    PaymentDate = DateTime.SpecifyKind(new DateTime(2025, 4, 20), DateTimeKind.Utc),
                    PaymentMethod = "Cash"
                }
            );

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Hotel)
                .WithMany()
                .HasForeignKey(e => e.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, UserId = 1, HotelId = 1, Position = "Manager" },
                new Employee { Id = 2, UserId = 2, HotelId = 2, Position = "Receptionist" }
            );

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Hotel)
                .WithMany(h => h.Reviews)
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>().HasData(
                new Review { Id = 1, HotelId = 1, UserId = 2, Rating = 5, Comment = "Amazing experience!" },
                new Review { Id = 2, HotelId = 2, UserId = 2, Rating = 4, Comment = "Great service, but the room was small." }
            );

            base.OnModelCreating(modelBuilder);
        }

    }
}
