using SteamAPI.Models.CompanyDTOs;
using SteamAPI.Models.GameDTOs;
using SteamAPI.Models.UserDTOs;

namespace SteamAPI.Models.GenreDTOs
{
    public class GenreDTO
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; }

        public ICollection<GameBaseDTO?> Games { get; set; } = new List<GameBaseDTO?>();
    }
}