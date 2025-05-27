using absensi_api.Data;
using absensi_api.Interface;
using absensi_api.Models;
using absensi_api.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace absensi_api.Repositories
{
    public class CheckInLogRepository : ICheckInLogRepository
    {
        private readonly AppDBContext _context;
        public CheckInLogRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CheckInLog>> GetAll()
        {
            return await _context.CheckInLogs.ToListAsync();
        }
        public async Task<CheckInLog?> GetById(int id)
        {
            var data = await _context.CheckInLogs
                .Where(r => r.CheckInLogId == id)
                .FirstOrDefaultAsync();

            return data is null ? null : data;
        }
        public async Task<int> Add(CheckInLogViewModel checkInLogVM)
        {
            var data = new CheckInLog
            {
                time_in = checkInLogVM.time_in,
                photo = checkInLogVM.photo,
                location = checkInLogVM.location
            };

            _context.CheckInLogs.Add(data);
            return await _context.SaveChangesAsync() > 0 ? 1 : 0;
        }
        public async Task<int> Update(string id, CheckInLogViewModel checkInLogVM)
        {
            var data = await _context.CheckInLogs.FindAsync(Convert.ToInt64(id));
            if (data is null)
                return 0;

            data.time_in = checkInLogVM.time_in;
            data.photo = checkInLogVM.photo;
            data.location = checkInLogVM.location;
            return await _context.SaveChangesAsync() > 0 ? 1 : 0;
        }
        public async Task<int> Delete(int id)
        {
            var data = await _context.CheckInLogs.FindAsync(id);
            if (data is null)
                return 0;

            _context.CheckInLogs.Remove(data);
            return await _context.SaveChangesAsync() > 0 ? 1 : 0;
        }
    }
}