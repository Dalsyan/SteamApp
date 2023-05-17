using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SteamDomain
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Nickname { get; set; }

        public Account Account { get; set; }

        public ICollection<Game> Games { get; set; } = new List<Game>();
        public ICollection<Vote> Votes { get; set; } = new List<Vote>();
    }
}
