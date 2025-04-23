using HotelBookingApi.Models;

namespace HotelBookingApi.Repositories
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetAll(CancellationToken cancellationToken);
        Task<Payment?> GetById(int id, CancellationToken cancellationToken);
        Task<Payment> Add(Payment payment, CancellationToken cancellationToken);
        Task<Payment> Update(Payment payment, CancellationToken cancellationToken);
        Task<bool> Delete(int id, CancellationToken cancellationToken);
    }
}
