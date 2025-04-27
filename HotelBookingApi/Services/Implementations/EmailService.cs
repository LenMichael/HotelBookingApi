using HotelBookingApi.Services.Interfaces;
using System;

namespace HotelBookingApi.Services.Implementations
{
    public class EmailService : IEmailService
    {
        public void SendBookingEmail(int bookingId, string customerEmail)
        {
            // Here will be the logic for sending emails
            Console.WriteLine("mplamplamplamplaaaaa");
            Console.WriteLine($"Email sent to {customerEmail} for booking ID: {bookingId}");
        }
    }
}
