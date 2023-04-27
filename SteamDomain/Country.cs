
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SteamDomain
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }
        [Required]
        public string CountryName { get; set; }

        public ICollection<Account> Accounts { get; set; } = new List<Account>();

        public ICollection<Company?> Companies { get; set; } = new List<Company?>();

        public ICollection<Server> Servers { get; set; } = new List<Server>();

        public ICollection<Developer?> Developers { get; set; } = new List<Developer?>();
    }
}

