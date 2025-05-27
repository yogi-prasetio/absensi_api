using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace absensi_api.Models
{
    public class CheckInLog
    {
        [Column("check_in_log_id")]
        public int CheckInLogId { get; set; }
        public TimeSpan? time_in { get; set; }
        public string? photo { get; set; }
        public string? location { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime? updated_at { get; set; } = DateTime.Now;
        public virtual AttendanceLog? AttendanceLog { get; set; }
    }
}