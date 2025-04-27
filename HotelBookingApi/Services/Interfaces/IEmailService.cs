namespace HotelBookingApi.Services.Interfaces
{
    public interface IEmailService
    {
        void SendBookingEmail(int bookingId, string customerEmail);
    }
}
