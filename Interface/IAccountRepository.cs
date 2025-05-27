using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using absensi_api.Models;
using absensi_api.ViewModels;

namespace absensi_api.Interface
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAll();
        Task<Account> GetById(int id);
        Task<int> Add(AccountViewModel.CreateAccount account);
        Task<int> Update(string id, AccountViewModel.UpdateAccount account);
        Task<int> Delete(int id);
    }
}