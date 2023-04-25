using SteamDomain;

namespace SteamAPI.Models
{
    public class AccountForCreationDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
