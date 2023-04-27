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
        public string Gender { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public ICollection<Developer> Developers { get; set; } = new List<Developer>();

        public ICollection<Server?> Servers { get; set; } = new List<Server?>();

        public ICollection<User?> Users { get; set; } = new List<User?>();
    }
}