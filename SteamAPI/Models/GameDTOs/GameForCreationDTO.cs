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
        public ICollection<DeveloperForCreationDTO> Developers { get; set; } = new List<DeveloperForCreationDTO>();
        public ICollection<ServerForCreationDTO?> Servers { get; set; } = new List<ServerForCreationDTO?>();
    }
}
