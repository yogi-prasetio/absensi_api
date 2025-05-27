using absensi_api.Data;
using absensi_api.Interface;
using absensi_api.Models;
using absensi_api.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace absensi_api.Repositories
{
    public class AttendanceLogRepository : IAttendanceLogRepository
    {
        private readonly AppDBContext _context;
        public AttendanceLogRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<AttendanceLogViewModel.AttendanceLogDTO>> GetAll()
        {
            var data = await _context.AttendanceLogs
        .Join(_context.CheckInLogs,
            al => al.CheckInLogId,
            ci => ci.CheckInLogId,
            (al, ci) => new { al, ci })
        .Join(_context.CheckOutLogs,
            combine => combine.al.CheckOutLogId,
            co => co.CheckOutLogId,
            (combine, co) => new AttendanceLogViewModel.AttendanceLogDTO
            {
                AttendanceLogId = combine.al.AttendanceLogId,
                AccountId = combine.al.AccountId,
                Date = combine.al.date,
                CheckInLogId = combine.ci.CheckInLogId,
                TimeIn = combine.ci.time_in,
                PhotoIn = combine.ci.photo,
                LocationIn = combine.ci.location,
                CheckOutLogId = co.CheckOutLogId,
                TimeOut = co.time_out,
                PhotoOut = co.photo,
                LocationOut = co.location,
            })
        .ToListAsync();

            return data;
        }
        public async Task<AttendanceLogViewModel.AttendanceLogDTO?> GetById(int id)
        {
            var data = await _context.AttendanceLogs
                .Join(_context.CheckInLogs,
                    al => al.CheckInLogId,
                    ci => ci.CheckInLogId,
                    (al, ci) => new { al, ci })
                .Join(_context.CheckOutLogs,
                    combine => combine.al.CheckOutLogId,
                    co => co.CheckOutLogId,
                    (combine, co) => new AttendanceLogViewModel.AttendanceLogDTO
                    {
                        AttendanceLogId = combine.al.AttendanceLogId,
                        AccountId = combine.al.AccountId,
                        Date = combine.al.date,
                        CheckInLogId = combine.ci.CheckInLogId,
                        TimeIn = combine.ci.time_in,
                        PhotoIn = combine.ci.photo,
                        LocationIn = combine.ci.location,
                        CheckOutLogId = co.CheckOutLogId,
                        TimeOut = co.time_out,
                        PhotoOut = co.photo,
                        LocationOut = co.location,
                    }).Where(r => r.AttendanceLogId == id)
                .FirstOrDefaultAsync();

            return data is null ? null : data;
        }
        public async Task<int> Add(AttendanceLogViewModel.CreateAttendance payload)
        {
            //Add data Check In
            var checkInLog = new CheckInLog
            {
                time_in = payload.time_in,
                photo = payload.photo,
                location = payload.location,
            };
            _context.CheckInLogs.Add(checkInLog);
            await _context.SaveChangesAsync();

            //Add data Check Out
            var checkOutLog = new CheckOutLog
            {
                time_out = null,
                photo = null,
                location = null,
            };
            _context.CheckOutLogs.Add(checkOutLog);
            await _context.SaveChangesAsync();

            //Add data Attendance
            var AttendanceLog = new AttendanceLog
            {
                AccountId = payload.account_id,
                CheckInLogId = checkInLog.CheckInLogId,
                CheckOutLogId = checkOutLog.CheckOutLogId,
                date = payload.date,
            };

            _context.AttendanceLogs.Add(AttendanceLog);
            return await _context.SaveChangesAsync() > 0 ? 1 : 0;
        }
        public async Task<int> Update(string id, AttendanceLogViewModel.UpdateAttendance AttendanceLogVM)
        {
            var data = await _context.AttendanceLogs.FindAsync(Convert.ToInt64(id));
            if (data is null)
                return 0;

            data.date = AttendanceLogVM.date;
            return await _context.SaveChangesAsync() > 0 ? 1 : 0;
        }
        // public async Task<int> Delete(int id)
        // {
        //     var data = await _context.AttendanceLogs.FindAsync(id);
        //     if (data is null)
        //         return 0;

        //     _context.AttendanceLogs.Remove(data);
        //     return await _context.SaveChangesAsync() > 0 ? 1 : 0;
        // }
    }
}