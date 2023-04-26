using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SteamDomain
{
    public class Company
    {
        public Company() 
        {
            Games = new List<Game>();
            Servers = new List<Server>();
            Countries = new List<Country>();
            Developers = new List<Developer>();
        }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public List<Game> Games { get; set; }
        public List<Server>? Servers { get; set; }
        public List<Country> Countries { get; set; }
        public List<Developer> Developers { get; set; }
    }
}