using absensi_api.Models;
using absensi_api.ViewModels;

namespace absensi_api.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);
        Task<int> Add(UserViewModel user);
        Task<int> Update(string id, UserViewModel user);
        Task<int> Delete(int id);
    }
}