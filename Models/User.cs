using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace absensi_api.Models
{
    public class User
    {
        [Column("user_id")]
        public int UserId { get; set; }
        public int nik { get; set; }
        public required string name { get; set; }
        public required string address { get; set; }
        public required string phone { get; set; }
        public required string email { get; set; }
        public string? image { get; set; }
        public string? location { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime? updated_at { get; set; } = DateTime.Now;

        public virtual Account? Account { get; set; }
    }
}