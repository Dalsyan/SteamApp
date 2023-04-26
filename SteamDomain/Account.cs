using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SteamDomain
{
    public class Account
    {
        [Key]
        public int EmailId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
