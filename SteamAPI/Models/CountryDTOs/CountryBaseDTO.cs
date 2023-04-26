using SteamDomain;

namespace SteamAPI.Models.CountryDTOs
{
    public class CountryBaseDTO
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public List<Server>? Servers { get; set; } = new List<Server>();
    }
}
