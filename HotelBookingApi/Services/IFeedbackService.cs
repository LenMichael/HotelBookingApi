using HotelBookingApi.Models;

namespace HotelBookingApi.Services
{
    public interface IFeedbackService
    {
        Task<IEnumerable<Feedback>> GetAllFeedback(CancellationToken cancellationToken);
        Task<Feedback?> GetFeedbackById(int id, CancellationToken cancellationToken);
        Task<Feedback> CreateFeedback(Feedback feedback, CancellationToken cancellationToken);
        Task<Feedback?> UpdateFeedback(int id, Feedback feedback, CancellationToken cancellationToken);
        Task<bool> DeleteFeedback(int id, CancellationToken cancellationToken);
    }
}
