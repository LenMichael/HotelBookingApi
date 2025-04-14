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

        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            return await _paymentRepository.GetAllAsync();
        }

        public async Task<Payment?> GetPaymentByIdAsync(int id)
        {
            return await _paymentRepository.GetByIdAsync(id);
        }

        public async Task<Payment> CreatePaymentAsync(Payment payment)
        {
            payment.PaymentDate = DateTime.UtcNow;
            return await _paymentRepository.AddAsync(payment);
        }

        public async Task<Payment?> UpdatePaymentAsync(int id, Payment payment)
        {
            var existingPayment = await _paymentRepository.GetByIdAsync(id);
            if (existingPayment == null) return null;

            existingPayment.Amount = payment.Amount;
            existingPayment.PaymentMethod = payment.PaymentMethod;
            existingPayment.PaymentDate = DateTime.UtcNow;

            return await _paymentRepository.UpdateAsync(existingPayment);
        }

        public async Task<bool> DeletePaymentAsync(int id)
        {
            return await _paymentRepository.DeleteAsync(id);
        }
    }
}
