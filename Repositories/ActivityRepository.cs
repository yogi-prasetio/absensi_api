using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using absensi_api.Data;
using absensi_api.Interface;
using absensi_api.Models;
using absensi_api.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace absensi_api.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly AppDBContext _context;
        public ActivityRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Activity?>> GetAll()
        {
            return await _context.Activities.ToListAsync();
        }
        public async Task<Activity?> GetById(int id)
        {
            var data = await _context.Activities
                .Where(r => r.ActivityId == id)
                .FirstOrDefaultAsync();

            return data is null ? null : data;
        }
        public async Task<int> Add(ActivityViewModel.CreateActivity activityVM)
        {
            var data = new Activity
            {
                activity = activityVM.activity,
                start_time = activityVM.start_time,
                end_time = activityVM.end_time,
                AttendanceLogId = activityVM.attendance_log_id
            };

            _context.Activities.Add(data);
            return await _context.SaveChangesAsync() > 0 ? 1 : 0;
        }
        public async Task<int> Update(string id, ActivityViewModel.UpdateActivity activityVM)
        {
            var data = await _context.Activities.FindAsync(Convert.ToInt64(id));
            if (data is null)
                return 0;

            data.activity = activityVM.activity;
            data.start_time = activityVM.start_time;
            data.end_time = activityVM.end_time;
            return await _context.SaveChangesAsync() > 0 ? 1 : 0;
        }
        public async Task<int> Delete(int id)
        {
            var data = await _context.Activities.FindAsync(id);
            if (data == null)
            {
                return 0;
            }
            _context.Activities.Remove(data);
            return await _context.SaveChangesAsync() > 0 ? 1 : 0;
        }
    }
}