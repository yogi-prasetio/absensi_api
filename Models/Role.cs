using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace absensi_api.Models
{
    public class Role
    {
        [Column("role_id")]
        public int RoleId { get; set; }
        public required string role_name { get; set; }
        public string? role_description { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime? updated_at { get; set; } = DateTime.Now;
        public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}