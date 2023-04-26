using SteamDomain;

namespace SteamAPI.Models.GameDTOs
{
    public class GameForUpdateDTO
    {
        public string Title { get; set; }
        public string Gender { get; set; }
        public List<User?> Users { get; set; } = new List<User?>();
        public List<Developer> Developers { get; set; } = new List<Developer>();
        public List<Server>? Servers { get; set; } = new List<Server>();
    }
}
