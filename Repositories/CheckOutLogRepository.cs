using absensi_api.Data;
using absensi_api.Interface;
using absensi_api.Models;
using absensi_api.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace absensi_api.Repositories
{
    public class CheckOutLogRepository : ICheckOutLogRepository
    {
        private readonly AppDBContext _context;
        public CheckOutLogRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CheckOutLog>> GetAll()
        {
            return await _context.CheckOutLogs.ToListAsync();
        }
        public async Task<CheckOutLog?> GetById(int id)
        {
            var data = await _context.CheckOutLogs
                .Where(r => r.CheckOutLogId == id)
                .FirstOrDefaultAsync();

            return data is null ? null : data;
        }
        public async Task<int> Add(CheckOutLogViewModel checkOutLogVM)
        {
            var data = new CheckOutLog
            {
                time_out = checkOutLogVM.time_out,
                photo = checkOutLogVM.photo,
                location = checkOutLogVM.location
            };

            _context.CheckOutLogs.Add(data);
            return await _context.SaveChangesAsync() > 0 ? 1 : 0;
        }
        public async Task<int> Update(string id, CheckOutLogViewModel checkOutLogVM)
        {
            var data = await _context.CheckOutLogs.FindAsync(Convert.ToInt64(id));
            if (data is null)
                return 0;

            data.time_out = checkOutLogVM.time_out;
            data.photo = checkOutLogVM.photo;
            data.location = checkOutLogVM.location;
            return await _context.SaveChangesAsync() > 0 ? 1 : 0;
        }
        public async Task<int> Delete(int id)
        {
            var data = await _context.CheckOutLogs.FindAsync(id);
            if (data is null)
                return 0;

            _context.CheckOutLogs.Remove(data);
            return await _context.SaveChangesAsync() > 0 ? 1 : 0;
        }
    }
}