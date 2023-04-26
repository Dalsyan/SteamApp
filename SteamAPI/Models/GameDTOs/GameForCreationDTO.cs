using SteamAPI.Models.DeveloperDTOs;
using SteamAPI.Models.ServerDTOs;
using SteamAPI.Models.UserDTOs;
using SteamDomain;

namespace SteamAPI.Models.GameDTOs
{
    public class GameForCreationDTO
    {
        public string Title { get; set; }
        public string Gender { get; set; }
        // public List<UserBaseDTO?> Users { get; set; } = new List<UserBaseDTO?>();
        public List<DeveloperBaseDTO> Developers { get; set; } = new List<DeveloperBaseDTO>();
        public List<ServerBaseDTO> Servers { get; set; } = new List<ServerBaseDTO>();
    }
}
