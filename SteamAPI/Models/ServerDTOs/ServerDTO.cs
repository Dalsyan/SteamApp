using SteamAPI.Models.CompanyDTOs;
using SteamAPI.Models.CountryDTOs;
using SteamAPI.Models.GameDTOs;
using SteamDomain;
using System.ComponentModel.DataAnnotations.Schema;

namespace SteamAPI.Models.ServerDTOs
{
    public class ServerDTO
    {
        public int ServerId { get; set; }
        public string ServerName { get; set; }

        public GameBaseDTO Game { get; set; }

        public CompanyBaseDTO Company { get; set; }

        public CountryBaseDTO Country { get; set; }
    }
}
