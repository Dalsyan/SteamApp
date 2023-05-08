using SteamAPI.Models.CompanyDTOs;
using SteamAPI.Models.DeveloperDTOs;
using SteamAPI.Models.ServerDTOs;
using SteamAPI.Models.UserDTOs;
using SteamDomain;

namespace SteamAPI.Models.GameDTOs
{
    public class GameUsersDTO
    {
        public string Title { get; set; }
        public ICollection<UserBaseDTO?> Users { get; set; } = new List<UserBaseDTO?>();
    }
}
