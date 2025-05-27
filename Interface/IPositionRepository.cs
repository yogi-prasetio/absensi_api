using absensi_api.Models;
using absensi_api.ViewModels;

namespace absensi_api.Interface
{
    public interface IPositionRepository
    {
        Task<IEnumerable<Position>> GetAll();
        Task<Position> GetById(int id);
        Task<int> Add(PositionViewModel checkInLog);
        Task<int> Update(string id, PositionViewModel checkInLog);
        Task<int> Delete(int id);
    }
}