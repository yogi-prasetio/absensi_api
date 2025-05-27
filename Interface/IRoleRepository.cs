using absensi_api.Models;
using absensi_api.ViewModels;

namespace absensi_api.Interface
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAll();
        Task<Role> GetById(int id);
        Task<int> Add(RoleViewModel role);
        Task<int> Update(string id, RoleViewModel role);
        Task<int> Delete(int id);
    }
}