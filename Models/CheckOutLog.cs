using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace absensi_api.Models
{
    public class CheckOutLog
    {
        [Column("check_out_log_id")]
        public int CheckOutLogId { get; set; }
        public TimeSpan? time_out { get; set; }
        public string? photo { get; set; }
        public string? location { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime? updated_at { get; set; } = DateTime.Now;
        public virtual AttendanceLog? AttendanceLog { get; set; }

    }
}