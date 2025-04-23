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

        public async Task<IEnumerable<Feedback>> GetAllFeedback(CancellationToken cancellationToken)
        {
            return await _feedbackRepository.GetAll(cancellationToken);
        }

        public async Task<Feedback?> GetFeedbackById(int id, CancellationToken cancellationToken)
        {
            return await _feedbackRepository.GetById(id, cancellationToken);
        }

        public async Task<Feedback> CreateFeedback(Feedback feedback, CancellationToken cancellationToken)
        {
            feedback.CreatedAt = DateTime.UtcNow;
            return await _feedbackRepository.Add(feedback, cancellationToken);
        }

        public async Task<Feedback?> UpdateFeedback(int id, Feedback feedback, CancellationToken cancellationToken)
        {
            var existingFeedback = await _feedbackRepository.GetById(id, cancellationToken);
            if (existingFeedback == null) return null;

            existingFeedback.Message = feedback.Message;
            existingFeedback.EmployeeId = feedback.EmployeeId;
            return await _feedbackRepository.Update(existingFeedback, cancellationToken);
        }

        public async Task<bool> DeleteFeedback(int id, CancellationToken cancellationToken)
        {
            return await _feedbackRepository.Delete(id, cancellationToken);
        }
    }
}
