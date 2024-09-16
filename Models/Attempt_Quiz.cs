using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MyanmarWisdomHubAPI.Models
{
    public class Attempt_Quiz
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int attempt_Id { get; set; }

        [Required]
        public string player_one_username { get; set; }

        public string? player_two_username { get; set; }

        [Required]
        public int player_one_score { get; set; }

        public int? player_two_score { get; set; }

        [Required]
        [Column("quiz_ids")]
        public string QuizIdsString { get; set; }

        // Not mapped to the database, used to represent the array of quiz IDs
        [NotMapped]
        public List<int> QuizIds
        {
            get
            {
                return QuizIdsString?.Split(',').Select(int.Parse).ToList() ?? new List<int>();
            }
            set
            {
                QuizIdsString = string.Join(",", value);
            }
        }
    }
}
