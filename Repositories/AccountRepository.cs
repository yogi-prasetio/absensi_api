using absensi_api.Data;
using absensi_api.Interface;
using absensi_api.Models;
using absensi_api.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace absensi_api.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDBContext _context;
        public AccountRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Account>> GetAll()
        {
            return await _context.Accounts.ToListAsync();
        }
        public async Task<Account?> GetById(int id)
        {
            var data = await _context.Accounts
                .Where(r => r.AccountId == id)
                .FirstOrDefaultAsync();

            return data is null ? null : data;
        }
        public async Task<int> Add(AccountViewModel.CreateAccount AccountVM)
        {
            var data = new Account
            {
                username = AccountVM.username,
                password = AccountVM.password
            };

            _context.Accounts.Add(data);
            return await _context.SaveChangesAsync() > 0 ? 1 : 0;
        }
        public async Task<int> Update(string id, AccountViewModel.UpdateAccount accountVM)
        {
            var data = await _context.Accounts.FindAsync(Convert.ToInt64(id));
            if (data is null)
                return 0;

            data.password = accountVM.password;
            data.RoleId = accountVM.role_id;
            data.PositionId = accountVM.position_id;
            return await _context.SaveChangesAsync() > 0 ? 1 : 0;
        }
        public async Task<int> Delete(int id)
        {
            var data = await _context.Accounts.FindAsync(id);
            if (data == null)
                return 0;
            _context.Accounts.Remove(data);
            return await _context.SaveChangesAsync() > 0 ? 1 : 0;
        }
    }
}