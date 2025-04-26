using HotelBookingApi.Models;

namespace HotelBookingApi.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> GetAllPayments(CancellationToken cancellationToken);
        Task<Payment?> GetPaymentById(int id, CancellationToken cancellationToken);
        Task<Payment> CreatePayment(Payment payment, CancellationToken cancellationToken);
        Task<Payment?> UpdatePayment(int id, Payment payment, CancellationToken cancellationToken);
        Task<bool> DeletePayment(int id, CancellationToken cancellationToken);
    }
}
