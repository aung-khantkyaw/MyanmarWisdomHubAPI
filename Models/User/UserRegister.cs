using System.ComponentModel.DataAnnotations;

namespace MyanmarWisdomHubAPI.Models.User
{
    public class UserRegister
    {
        [Required]
        public string username { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        public string password { get; set; }

        [Required]
        public string first_name { get; set; }

        [Required]
        public string last_name { get; set; }

        //[Required]
        //public string profile_url { get; set; }

        [Required]
        public IFormFile profileFile { get; set; }
    }
}
