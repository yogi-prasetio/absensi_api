using System.ComponentModel.DataAnnotations.Schema;

namespace absensi_api.Models
{
    public class Position
    {
        [Column("position_id")]
        public int PositionId { get; set; }
        public string position { get; set; }
        public string? description { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime? updated_at { get; set; } = DateTime.Now;
        public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}