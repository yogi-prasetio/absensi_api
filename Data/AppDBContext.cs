using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using absensi_api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace absensi_api.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Position> Position { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AttendanceLog> AttendanceLogs { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<CheckInLog> CheckInLogs { get; set; }
        public DbSet<CheckOutLog> CheckOutLogs { get; set; }

    }
}