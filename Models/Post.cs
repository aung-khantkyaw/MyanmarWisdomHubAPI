using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyanmarWisdomHubAPI.Models
{
    [Table("Post")]
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string title { get; set; }

        [Required]
        public string body { get; set; }

        [Required]
        public int user_id { get; set; }
    }
}
