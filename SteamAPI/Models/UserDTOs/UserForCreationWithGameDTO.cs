using SteamAPI.Models.GameDTOs;

namespace SteamAPI.Models.UserDTOs
{
    public class UserForCreationWithGameDTO
    {
        public string Nickname { get; set; } = string.Empty;
        public int CountryId { get; set; }
        public int EmailId { get; set; }
        public ICollection<VoteBaseDTO?> Games { get; set; } = new List<VoteBaseDTO?>();
    }
}
