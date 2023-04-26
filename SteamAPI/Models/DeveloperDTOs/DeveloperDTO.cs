using SteamDomain;

namespace SteamAPI.Models.DeveloperDTOs
{
    public class DeveloperDTO
    {
        public int DevId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CompanyId { get; set; }
        public int CountryId { get; set; }
    }
}
