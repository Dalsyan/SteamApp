using SteamAPI.Models.UserDTOs;
using SteamDomain;

namespace SteamAPI.Models.AccountDTOs
{
    public class AccountForCreationDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate { get; set; }
        public int CountryId { get; set; }
        public UserBaseDTO User { get; set; }
    }
}
