using SteamDomain;

namespace SteamAPI.Models.AccountDTOs
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
