using absensi_api.Data;
using absensi_api.Interface;
using absensi_api.Models;
using absensi_api.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace absensi_api.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDBContext _context;
        public RoleRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Role>> GetAll()
        {
            return await _context.Roles.ToListAsync();
        }
        public async Task<Role?> GetById(int id)
        {
            var data = await _context.Roles
                .Where(r => r.RoleId == id)
                .FirstOrDefaultAsync();

            return data is null ? null : data;
        }
        public async Task<int> Add(RoleViewModel roleVM)
        {
            var data = new Role
            {
                role_name = roleVM.name,
                role_description = roleVM.description
            };

            _context.Roles.Add(data);
            return await _context.SaveChangesAsync() > 0 ? 1 : 0;
        }
        public async Task<int> Update(string id, RoleViewModel roleVM)
        {
            var data = await _context.Roles.FindAsync(Convert.ToInt64(id));
            if (data is null)
                return 0;

            data.role_name = roleVM.name;
            data.role_description = roleVM.description;
            return await _context.SaveChangesAsync() > 0 ? 1 : 0;
        }
        public async Task<int> Delete(int id)
        {
            var data = await _context.Roles.FindAsync(id);
            if (data is null)
                return 0;

            _context.Roles.Remove(data);
            return await _context.SaveChangesAsync() > 0 ? 1 : 0;
        }
    }
}