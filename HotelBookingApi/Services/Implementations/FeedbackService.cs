using FluentValidation;
using HotelBookingApi.Models;
using HotelBookingApi.Repositories.Interfaces;
using HotelBookingApi.Services.Interfaces;

namespace HotelBookingApi.Services.Implementations
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IValidator<Feedback> _feedbackValidator;

        public FeedbackService(IFeedbackRepository feedbackRepository, IValidator<Feedback> feedbackValidator)
        {
            _feedbackRepository = feedbackRepository;
            _feedbackValidator = feedbackValidator;
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
            var validationResult = await _feedbackValidator.ValidateAsync(feedback, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

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
