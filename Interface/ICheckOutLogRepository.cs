using absensi_api.Models;
using absensi_api.ViewModels;

namespace absensi_api.Interface
{
    public interface ICheckOutLogRepository
    {
        Task<IEnumerable<CheckOutLog>> GetAll();
        Task<CheckOutLog> GetById(int id);
        Task<int> Add(CheckOutLogViewModel checkOutLog);
        Task<int> Update(string id, CheckOutLogViewModel checkOutLog);
        Task<int> Delete(int id);
    }
}