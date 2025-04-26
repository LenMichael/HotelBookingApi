using HotelBookingApi.Data;
using HotelBookingApi.Models;
using HotelBookingApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingApi.Repositories.Implementations
{
    public class MaintenanceRequestRepository : IMaintenanceRequestRepository
    {
        private readonly ApiContext _context;

        public MaintenanceRequestRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MaintenanceRequest>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.MaintenanceRequests.ToListAsync(cancellationToken);
        }

        public async Task<MaintenanceRequest?> GetById(int id, CancellationToken cancellationToken)
        {
            return await _context.MaintenanceRequests.FindAsync(id, cancellationToken);
        }

        public async Task<MaintenanceRequest> Add(MaintenanceRequest request, CancellationToken cancellationToken)
        {
            _context.MaintenanceRequests.Add(request);
            await _context.SaveChangesAsync(cancellationToken);
            return request;
        }

        public async Task<MaintenanceRequest> Update(MaintenanceRequest request, CancellationToken cancellationToken)
        {
            _context.MaintenanceRequests.Update(request);
            await _context.SaveChangesAsync(cancellationToken);
            return request;
        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken)
        {
            var request = await _context.MaintenanceRequests.FindAsync(id, cancellationToken);
            if (request == null) return false;

            _context.MaintenanceRequests.Remove(request);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
