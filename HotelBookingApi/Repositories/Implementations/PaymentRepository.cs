using HotelBookingApi.Data;
using HotelBookingApi.Models;
using HotelBookingApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

namespace HotelBookingApi.Repositories.Implementations
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApiContext _context;

        public PaymentRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Payment>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Payments.Include(p => p.Booking).ToListAsync(cancellationToken);
        }

        public async Task<Payment?> GetById(int id, CancellationToken cancellationToken)
        {
            return await _context.Payments.Include(p => p.Booking).FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task<Payment> Add(Payment payment, CancellationToken cancellationToken)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync(cancellationToken);
            return payment;
        }

        public async Task<Payment> Update(Payment payment, CancellationToken cancellationToken)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync(cancellationToken);
            return payment;
        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken)
        {
            var payment = await _context.Payments.FindAsync(id, cancellationToken);
            if (payment == null) return false;

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
