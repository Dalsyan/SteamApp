using SteamAPI.Models.CompanyDTOs;
using SteamAPI.Models.DeveloperDTOs;
using SteamAPI.Models.GenreDTOs;
using SteamAPI.Models.ServerDTOs;
using SteamAPI.Models.UserDTOs;
using SteamDomain;

namespace SteamAPI.Models.VoteDTOs
{
    public class VoteDTO
    {
        public int VoteId { get; set; }
        public double Score { get; set; }
    }
}
