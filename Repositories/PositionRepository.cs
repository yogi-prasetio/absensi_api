using absensi_api.Data;
using absensi_api.Interface;
using absensi_api.Models;
using absensi_api.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace absensi_api.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        private readonly AppDBContext _context;
        public PositionRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Position>> GetAll()
        {
            return await _context.Position.ToListAsync();
        }
        public async Task<Position?> GetById(int id)
        {
            var data = await _context.Position
                .Where(r => r.PositionId == id)
                .FirstOrDefaultAsync();

            return data is null ? null : data;
        }
        public async Task<int> Add(PositionViewModel positionVM)
        {
            var data = new Position
            {
                position = positionVM.name,
                description = positionVM.description
            };

            _context.Position.Add(data);
            return await _context.SaveChangesAsync() > 0 ? 1 : 0;
        }
        public async Task<int> Update(string id, PositionViewModel positionVM)
        {
            var data = await _context.Position.FindAsync(Convert.ToInt64(id));
            if (data is null)
                return 0;

            data.position = positionVM.name;
            data.description = positionVM.description;
            return await _context.SaveChangesAsync() > 0 ? 1 : 0;
        }
        public async Task<int> Delete(int id)
        {
            var data = await _context.Position.FindAsync(id);
            if (data is null)
                return 0;

            _context.Position.Remove(data);
            return await _context.SaveChangesAsync() > 0 ? 1 : 0;
        }
    }
}