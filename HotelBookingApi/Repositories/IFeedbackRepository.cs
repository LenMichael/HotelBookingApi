using HotelBookingApi.Models;

namespace HotelBookingApi.Repositories
{
    public interface IFeedbackRepository
    {
        Task<IEnumerable<Feedback>> GetAll(CancellationToken cancellationToken);
        Task<Feedback?> GetById(int id, CancellationToken cancellationToken);
        Task<Feedback> Add(Feedback feedback, CancellationToken cancellationToken);
        Task<Feedback> Update(Feedback feedback, CancellationToken cancellationToken);
        Task<bool> Delete(int id, CancellationToken cancellationToken);
    }
}
