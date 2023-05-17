using SteamAPI.Models.CompanyDTOs;
using SteamAPI.Models.GenreDTOs;
using SteamAPI.Models.UserDTOs;

namespace SteamAPI.Models.GameDTOs
{
    public class VoteBaseDTO
    {
        public int GameId { get; set; }
        public string Title { get; set; }
        public ICollection<GenreBaseDTO?> Genres { get; set; } = new List<GenreBaseDTO?>();
        public int CompanyId { get; set; }
        public double Score { get; set; }
    }
}
