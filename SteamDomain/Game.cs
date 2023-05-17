using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SteamDomain
{
    public class Game
    {
        [Key]
        public int GameId { get; set; }
        [Required]
        public string Title { get; set; }
        public bool HasOnline { get; set; } = false;
        [Range(1, 5)]
        public double Score { get; set; } = 0.0;
        public int HanVotado { get; set; } = 0;

        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public ICollection<Genre> Genres { get; set; } = new List<Genre>();
        public ICollection<Developer> Developers { get; set; } = new List<Developer>();
        public ICollection<Server?> Servers { get; set; } = new List<Server?>();
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Vote> Votes { get;  set; } = new List<Vote>();
    }
}