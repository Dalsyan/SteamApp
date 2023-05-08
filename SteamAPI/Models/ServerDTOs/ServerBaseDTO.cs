using SteamAPI.Models.CountryDTOs;

namespace SteamAPI.Models.ServerDTOs
{
    public class ServerBaseDTO
    {
        public string ServerName { get; set; }
        public CountryBaseDTO Country { get; set; }
    }
}
