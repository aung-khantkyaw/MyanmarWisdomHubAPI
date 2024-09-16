using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyanmarWisdomHubAPI.Models
{
    [Table("Notification")]
    public class Notification
    {
        [Key]
        public int Id { get; set; }

        public string? from_user_name { get; set; }

        public string? to_user_name { get; set; }

        public string? notification_type { get; set; }

        public string? content { get; set; }

        [Required]
        public DateTime created_at { get; set; } = DateTime.Now;

        public bool? is_answer { get; set; } = false;

        public int? attempt_Id { get; set; }
    }
}
