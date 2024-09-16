using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyanmarWisdomHubAPI.Models
{
    [Table("quiz")]
    public class Quiz
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string question { get; set; }

        [Required]
        public string answer { get; set; }

        [Required]
        public string option_one { get; set; }

        [Required]
        public string option_two { get; set; }

        [Required]
        public string option_three { get; set; }

        [Required]
        public string option_four { get; set; }
    }
}
