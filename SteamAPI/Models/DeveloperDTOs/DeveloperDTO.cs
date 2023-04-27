using SteamAPI.Models.CompanyDTOs;
using SteamAPI.Models.CountryDTOs;
using SteamAPI.Models.GameDTOs;
using SteamDomain;
using System.ComponentModel.DataAnnotations.Schema;

namespace SteamAPI.Models.DeveloperDTOs
{
    public class DeveloperDTO
    {
        public int DevId { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }

        public CountryBaseDTO Country { get; set; }

        public CompanyBaseDTO Company { get; set; }

        public ICollection<GameBaseDTO?> Games { get; set; } = new List<GameBaseDTO?>();
    }
}
