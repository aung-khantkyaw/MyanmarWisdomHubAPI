using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyanmarWisdomHubAPI.Models.User
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string username { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string password { get; set; }

        [Required]
        public string first_name { get; set; }

        [Required]
        public string last_name { get; set; }

        [Required]
        public DateTime created_at { get; set; } = DateTime.Now;

        [Required]
        public DateTime updated_at { get; set; } = DateTime.Now;

        [Required]
        public string profile_url { get; set; }

    }
}
