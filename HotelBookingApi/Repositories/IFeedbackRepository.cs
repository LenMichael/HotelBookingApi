using HotelBookingApi.Models;

namespace HotelBookingApi.Repositories
{
    public interface IFeedbackRepository
    {
        Task<IEnumerable<Feedback>> GetAllAsync();
        Task<Feedback?> GetByIdAsync(int id);
        Task<Feedback> AddAsync(Feedback feedback);
        Task<Feedback> UpdateAsync(Feedback feedback);
        Task<bool> DeleteAsync(int id);
    }
}
