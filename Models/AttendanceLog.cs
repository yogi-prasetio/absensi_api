using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace absensi_api.Models
{
    public class AttendanceLog
    {
        [Column("attendance_log_id")]
        public int AttendanceLogId { get; set; }
        public DateTime date { get; set; }
        [ForeignKey("CheckInLog")]
        [Column("check_in_log_id")]
        public int? CheckInLogId { get; set; }
        [ForeignKey("CheckOutLog")]
        [Column("check_out_log_id")]
        public int? CheckOutLogId { get; set; }
        [ForeignKey("Account")]
        [Column("account_id")]
        public int AccountId { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime? updated_at { get; set; } = DateTime.Now;
        public virtual CheckInLog? CheckInLog { get; set; }
        public virtual CheckOutLog? CheckOutLog { get; set; }
        public virtual Account? Account { get; set; }
        public virtual ICollection<Activity> Activities { get; set; } = new List<Activity>();
    }
}