using SteamDomain;

namespace SteamAPI.Models.CountryDTOs
{
    public class CountryForUpdateDTO
    {
        public string CountryName { get; set; }
        public List<Server>? Servers { get; set; } = new List<Server>();
    }
}
