using HotelBookingApi.Models;
using HotelBookingApi.Repositories;

namespace HotelBookingApi.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackService(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public async Task<IEnumerable<Feedback>> GetAllFeedbackAsync()
        {
            return await _feedbackRepository.GetAllAsync();
        }

        public async Task<Feedback?> GetFeedbackByIdAsync(int id)
        {
            return await _feedbackRepository.GetByIdAsync(id);
        }

        public async Task<Feedback> CreateFeedbackAsync(Feedback feedback)
        {
            feedback.CreatedAt = DateTime.UtcNow;
            return await _feedbackRepository.AddAsync(feedback);
        }

        public async Task<Feedback?> UpdateFeedbackAsync(int id, Feedback feedback)
        {
            var existingFeedback = await _feedbackRepository.GetByIdAsync(id);
            if (existingFeedback == null) return null;

            existingFeedback.Message = feedback.Message;
            existingFeedback.EmployeeId = feedback.EmployeeId;
            return await _feedbackRepository.UpdateAsync(existingFeedback);
        }

        public async Task<bool> DeleteFeedbackAsync(int id)
        {
            return await _feedbackRepository.DeleteAsync(id);
        }
    }
}
