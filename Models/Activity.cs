using System.ComponentModel.DataAnnotations.Schema;

namespace absensi_api.Models
{
    [Table("Activities")]
    public class Activity
    {
        [Column("activity_id")]
        public int ActivityId { get; set; }
        public required string activity { get; set; }
        public TimeSpan start_time { get; set; }
        public TimeSpan end_time { get; set; }
        [ForeignKey("AttendanceLog")]
        [Column("attendance_log_id")]
        public int AttendanceLogId { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime? updated_at { get; set; } = DateTime.Now;
        public virtual AttendanceLog? CheckInLog { get; set; }
    }
}