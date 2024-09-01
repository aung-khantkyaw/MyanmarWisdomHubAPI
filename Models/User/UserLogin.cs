using System.ComponentModel.DataAnnotations;

namespace MyanmarWisdomHubAPI.Models.User
{
    public class UserLogin
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}
