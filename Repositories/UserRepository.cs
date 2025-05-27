using absensi_api.Data;
using absensi_api.Interface;
using absensi_api.Models;
using absensi_api.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace absensi_api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDBContext _context;
        public UserRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User?> GetById(int id)
        {
            var data = await _context.Users
                .Where(r => r.UserId == id)
                .FirstOrDefaultAsync();

            return data is null ? null : data;
        }
        public async Task<int> Add(UserViewModel userVM)
        {
            var data = new User
            {
                nik = userVM.nik,
                name = userVM.name,
                address = userVM.address,
                phone = userVM.phone,
                email = userVM.email,
                location = userVM.location
            };

            _context.Users.Add(data);
            return await _context.SaveChangesAsync() > 0 ? 1 : 0;
        }
        public async Task<int> Update(string id, UserViewModel UserVM)
        {
            var data = await _context.Users.FindAsync(Convert.ToInt64(id));
            if (data is null)
                return 0;

            data.nik = UserVM.nik;
            data.name = UserVM.name;
            data.address = UserVM.address;
            data.phone = UserVM.phone;
            data.email = UserVM.email;
            data.location = UserVM.location;
            return await _context.SaveChangesAsync() > 0 ? 1 : 0;
        }
        public async Task<int> Delete(int id)
        {
            var data = await _context.Users.FindAsync(id);
            if (data is null)
                return 0;

            _context.Users.Remove(data);
            return await _context.SaveChangesAsync() > 0 ? 1 : 0;
        }
    }
}