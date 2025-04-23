using HotelBookingApi.Models;
using HotelBookingApi.Repositories;

namespace HotelBookingApi.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<IEnumerable<Payment>> GetAllPayments(CancellationToken cancellationToken)
        {
            return await _paymentRepository.GetAll(cancellationToken);
        }

        public async Task<Payment?> GetPaymentById(int id, CancellationToken cancellationToken)
        {
            return await _paymentRepository.GetById(id, cancellationToken);
        }

        public async Task<Payment> CreatePayment(Payment payment, CancellationToken cancellationToken)
        {
            payment.PaymentDate = DateTime.UtcNow;
            return await _paymentRepository.Add(payment, cancellationToken);
        }

        public async Task<Payment?> UpdatePayment(int id, Payment payment, CancellationToken cancellationToken)
        {
            var existingPayment = await _paymentRepository.GetById(id, cancellationToken);
            if (existingPayment == null) return null;

            existingPayment.Amount = payment.Amount;
            existingPayment.PaymentMethod = payment.PaymentMethod;
            existingPayment.PaymentDate = DateTime.UtcNow;

            return await _paymentRepository.Update(existingPayment, cancellationToken);
        }

        public async Task<bool> DeletePayment(int id, CancellationToken cancellationToken)
        {
            return await _paymentRepository.Delete(id, cancellationToken);
        }
    }
}
