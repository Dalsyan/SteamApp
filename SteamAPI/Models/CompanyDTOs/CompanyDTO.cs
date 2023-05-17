using SteamAPI.Models.CountryDTOs;
using SteamAPI.Models.DeveloperDTOs;
using SteamAPI.Models.GameDTOs;
using SteamAPI.Models.ServerDTOs;
using SteamDomain;

namespace SteamAPI.Models.CompanyDTOs
{
    public class CompanyDTO
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }

        public ICollection<VoteBaseDTO?> Games { get; set; } = new List<VoteBaseDTO?>();

        public ICollection<ServerBaseDTO?> Servers { get; set; } = new List<ServerBaseDTO?>();

        public ICollection<CountryBaseDTO> Countries { get; set; } = new List<CountryBaseDTO>();

        public ICollection<DeveloperBaseDTO?> Developers { get; set; } = new List<DeveloperBaseDTO?>();
    }
}
