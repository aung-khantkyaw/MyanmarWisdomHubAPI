using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyanmarWisdomHubAPI.Models
{
    [Table("Riddle")]
    public class Riddle
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string riddle {  get; set; }

        [Required]
        public string answer { get; set; }
    }
}
