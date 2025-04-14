using HotelBookingApi.Data;
using HotelBookingApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingApi.Repositories
{
    public class MaintenanceRequestRepository : IMaintenanceRequestRepository
    {
        private readonly ApiContext _context;

        public MaintenanceRequestRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MaintenanceRequest>> GetAllAsync()
        {
            return await _context.MaintenanceRequests.ToListAsync();
        }

        public async Task<MaintenanceRequest> GetByIdAsync(int id)
        {
            return await _context.MaintenanceRequests.FindAsync(id);
        }

        public async Task<MaintenanceRequest> AddAsync(MaintenanceRequest request)
        {
            _context.MaintenanceRequests.Add(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<MaintenanceRequest> UpdateAsync(MaintenanceRequest request)
        {
            _context.MaintenanceRequests.Update(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var request = await _context.MaintenanceRequests.FindAsync(id);
            if (request == null) return false;

            _context.MaintenanceRequests.Remove(request);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
