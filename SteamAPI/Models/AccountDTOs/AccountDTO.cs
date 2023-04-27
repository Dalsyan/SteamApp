using SteamAPI.Models.CountryDTOs;
using SteamAPI.Models.UserDTOs;
using SteamDomain;

namespace SteamAPI.Models.AccountDTOs
{
    public class AccountDTO
    {
        public int EmailId { get; set; }
        public string Email { get; set; }
        // public string Password { get; set; }
        public DateTime CreationDate { get; set; }
        public CountryBaseDTO? Country { get; set; }
        public UserBaseDTO User { get; set; }
    }
}
