using SteamAPI.Models.CompanyDTOs;
using SteamAPI.Models.DeveloperDTOs;
using SteamAPI.Models.GenreDTOs;
using SteamAPI.Models.ServerDTOs;
using SteamAPI.Models.UserDTOs;
using SteamDomain;

namespace SteamAPI.Models.GameDTOs
{
    public class VoteForCreationDTO
    {
        public string Title { get; set; }
        public int CompanyId { get; set; }

        public ICollection<GenreBaseDTO?> Genres { get; set; } = new List<GenreBaseDTO?>();
        public ICollection<DeveloperForCreationDTO> Developers { get; set; } = new List<DeveloperForCreationDTO>();
        public ICollection<ServerForCreationDTO?> Servers { get; set; } = new List<ServerForCreationDTO?>();
    }
}
