using absensi_api.Models;
using absensi_api.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace absensi_api.Interface
{
    public interface IActivityRepository
    {
        Task<IEnumerable<Models.Activity>> GetAll();
        Task<Models.Activity> GetById(int id);
        Task<int> Add(ActivityViewModel.CreateActivity checkInLog);
        Task<int> Update(string id, ActivityViewModel.UpdateActivity checkInLog);
        Task<int> Delete(int id);
    }
}