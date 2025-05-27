
using absensi_api.Models;
using absensi_api.ViewModels;

namespace absensi_api.Interface
{
    public interface ICheckInLogRepository
    {
        Task<IEnumerable<CheckInLog>> GetAll();
        Task<CheckInLog> GetById(int id);
        Task<int> Add(CheckInLogViewModel checkInLog);
        Task<int> Update(string id, CheckInLogViewModel checkInLog);
        Task<int> Delete(int id);
    }
}