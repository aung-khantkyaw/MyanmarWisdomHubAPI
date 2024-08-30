using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyanmarWisdomHubAPI.Models
{
    [Table("Proverb")]
    public class Proverb
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string proverb_burmese { get; set; }

        [Required]
        public string meaning_burmese { get; set; }

        [Required]
        [MaxLength(100)]
        public string proverb_english { get; set; }

        [Required]
        public string meaning_english { get; set; }
    }

}
