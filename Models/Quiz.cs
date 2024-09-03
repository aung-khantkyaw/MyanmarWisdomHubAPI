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
        public string option1 { get; set; }

        [Required]
        public string option2 { get; set; }

        [Required]
        public string option3 { get; set; }

        [Required]
        public string option4 { get; set; }
    }
}
