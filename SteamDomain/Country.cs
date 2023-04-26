
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SteamDomain
{
    public class Country
    {
        
        public Country()
        { 
            Companies = new List<Company>();
            Servers = new List<Server>();
            Users = new List<User>();
            Developers = new List<Developer>();
        }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public List<User> Users { get; set; }
        public List<Company> Companies { get; set; }
        public List<Server>? Servers { get; set; }
        public List<Developer> Developers { get; set; }
    }
}

