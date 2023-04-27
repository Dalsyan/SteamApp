using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SteamDomain
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }
        [Required]
        public string CompanyName { get; set; }

        public ICollection<Game?> Games { get; set; } = new List<Game?>();

        public ICollection<Server?> Servers { get; set; } = new List<Server?>();

        public ICollection<Country> Countries { get; set; } = new List<Country>();

        public ICollection<Developer?> Developers { get; set; } = new List<Developer?>();
    }
}