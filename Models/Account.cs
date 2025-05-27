using System.ComponentModel.DataAnnotations.Schema;

namespace absensi_api.Models
{
    public class Account
    {
        [Column("account_id")]
        public int AccountId { get; set; }
        public required string username { get; set; }
        public required string password { get; set; }
        public string? otp { get; set; }
        [ForeignKey("User")]
        [Column("user_id")]
        public int UserId { get; set; }
        [ForeignKey("Role")]
        [Column("role_id")]
        public int RoleId { get; set; }
        [ForeignKey("Position")]
        [Column("position_id")]
        public int? PositionId { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime? updated_at { get; set; } = DateTime.Now;
        public virtual User? User { get; set; }

        public virtual Role? Role { get; set; }
        public virtual Position? Position { get; set; }
        public virtual ICollection<AttendanceLog> AttendanceLogs { get; set; } = new List<AttendanceLog>();
    }
}