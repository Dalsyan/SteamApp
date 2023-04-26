using SteamDomain;

namespace SteamAPI.Models.UserDTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Nickname { get; set; } = string.Empty;
        public int CountryId { get; set; }
        public Account EmailId { get; set; }
        public List<Game?> Games { get; set; } = new List<Game?>();
    }
}
