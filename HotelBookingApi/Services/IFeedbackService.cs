using HotelBookingApi.Models;

namespace HotelBookingApi.Services
{
    public interface IFeedbackService
    {
        Task<IEnumerable<Feedback>> GetAllFeedbackAsync();
        Task<Feedback?> GetFeedbackByIdAsync(int id);
        Task<Feedback> CreateFeedbackAsync(Feedback feedback);
        Task<Feedback?> UpdateFeedbackAsync(int id, Feedback feedback);
        Task<bool> DeleteFeedbackAsync(int id);
    }
}
