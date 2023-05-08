using SteamDomain;

namespace SteamAPI.Models.ServerDTOs
{
    public class ServerForCreationDTO
    {
        public string ServerName { get; set; }
        public int CompanyId { get; set; }
        public int CountryId { get; set; }
        // public int GameId { get; set; }
    }
}
