using SteamAPI.Models.CompanyDTOs;
using SteamAPI.Models.DeveloperDTOs;
using SteamAPI.Models.GenreDTOs;
using SteamAPI.Models.ServerDTOs;
using SteamAPI.Models.UserDTOs;
using SteamDomain;

namespace SteamAPI.Models.GameDTOs
{
    public class GameDTO
    {
        public int GameId { get; set; }
        public string Title { get; set; }

        public CompanyBaseDTO Company { get; set; }

        public ICollection<GenreBaseDTO?> Genres { get; set; } = new List<GenreBaseDTO?>();
        public ICollection<UserBaseDTO?> Users { get; set; } = new List<UserBaseDTO?>();
        public ICollection<DeveloperBaseDTO> Developers { get; set; } = new List<DeveloperBaseDTO>();
        public ICollection<ServerBaseDTO?> Servers { get; set; } = new List<ServerBaseDTO?>();
    }
}
