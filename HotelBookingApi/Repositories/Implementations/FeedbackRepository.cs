using HotelBookingApi.Data;
using HotelBookingApi.Models;
using HotelBookingApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingApi.Repositories.Implementations
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly ApiContext _context;

        public FeedbackRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Feedback>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Feedbacks.Include(f => f.Employee).ToListAsync(cancellationToken);
        }

        public async Task<Feedback?> GetById(int id, CancellationToken cancellationToken)
        {
            return await _context.Feedbacks.Include(f => f.Employee).FirstOrDefaultAsync(f => f.Id == id, cancellationToken);
        }

        public async Task<Feedback> Add(Feedback feedback, CancellationToken cancellationToken)
        {
            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync(cancellationToken);
            return feedback;
        }

        public async Task<Feedback> Update(Feedback feedback, CancellationToken cancellationToken)
        {
            _context.Feedbacks.Update(feedback);
            await _context.SaveChangesAsync(cancellationToken);
            return feedback;
        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken)
        {
            var feedback = await _context.Feedbacks.FindAsync(id, cancellationToken);
            if (feedback == null) return false;

            _context.Feedbacks.Remove(feedback);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
