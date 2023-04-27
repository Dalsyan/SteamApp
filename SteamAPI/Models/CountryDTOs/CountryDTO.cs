using SteamAPI.Models.AccountDTOs;
using SteamAPI.Models.CompanyDTOs;
using SteamAPI.Models.DeveloperDTOs;
using SteamAPI.Models.ServerDTOs;
using SteamDomain;

namespace SteamAPI.Models.CountryDTOs
{
    public class CountryDTO
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public ICollection<AccountBaseDTO> Accounts { get; set; } = new List<AccountBaseDTO>();
        public ICollection<CompanyBaseDTO?> Companies { get; set; } = new List<CompanyBaseDTO?>();
        public ICollection<ServerBaseDTO> Servers { get; set; } = new List<ServerBaseDTO>();
        public ICollection<DeveloperBaseDTO?> Developers { get; set; } = new List<DeveloperBaseDTO?>();
    }
}
