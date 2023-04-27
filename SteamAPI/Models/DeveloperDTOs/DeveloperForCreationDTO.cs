using SteamAPI.Models.GameDTOs;

namespace SteamAPI.Models.DeveloperDTOs
{
    public class DeveloperForCreationDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CompanyId { get; set; }
        public int CountryId { get; set; }
        public ICollection<GameBaseDTO?> Games { get; set; } = new List<GameBaseDTO?>();
    }
}
