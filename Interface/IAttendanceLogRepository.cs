using absensi_api.ViewModels;

namespace absensi_api.Interface
{
    public interface IAttendanceLogRepository
    {
        Task<IEnumerable<AttendanceLogViewModel.AttendanceLogDTO>> GetAll();
        Task<AttendanceLogViewModel.AttendanceLogDTO> GetById(int id);
        Task<int> Add(AttendanceLogViewModel.CreateAttendance attendanceLog);
        Task<int> Update(string id, AttendanceLogViewModel.UpdateAttendance attendanceLog);
        // Task<int> Delete(int id);
    }
}