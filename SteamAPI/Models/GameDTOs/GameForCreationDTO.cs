using SteamAPI.Models.CompanyDTOs;
using SteamAPI.Models.DeveloperDTOs;
using SteamAPI.Models.ServerDTOs;
using SteamAPI.Models.UserDTOs;
using SteamDomain;

namespace SteamAPI.Models.GameDTOs
{
    public class GameForCreationDTO
    {
        public string Title { get; set; }
        public string? Gender { get; set; }
        public int CompanyId { get; set; }
        // public CompanyBaseDTO Company { get; set; }
        // public ICollection<UserBaseDTO?> Users { get; set; } = new List<UserBaseDTO?>();
        // public ICollection<DeveloperBaseDTO> Developers { get; set; } = new List<DeveloperBaseDTO>();
        // public ICollection<ServerBaseDTO?> Servers { get; set; } = new List<ServerBaseDTO?>();
    }
}
