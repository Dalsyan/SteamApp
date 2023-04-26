using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SteamDomain
{
    public class Game
    {        
        public Game() 
        {
            Users = new List<User?>();
            Developers = new List<Developer>();
            Servers = new List<Server>();
        }
        
        public int GameId { get; set; }
        public string Title { get; set; }
        public string Gender { get; set; }
        public int CompanyId { get; set; }
        public List<Developer> Developers { get; set; }
        public List<Server>? Servers { get; set; }
        public List<User?> Users { get; set; }
    }
}