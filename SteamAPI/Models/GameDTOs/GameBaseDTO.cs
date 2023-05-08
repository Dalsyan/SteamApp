using SteamAPI.Models.CompanyDTOs;

namespace SteamAPI.Models.GameDTOs
{
    public class GameBaseDTO
    {
        public string Title { get; set; }
        public string Gender { get; set; }
        public int CompanyId { get; set; }
    }
}
