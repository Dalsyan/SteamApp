using SteamAPI.Models.CompanyDTOs;
using SteamAPI.Models.GenreDTOs;
using SteamAPI.Models.UserDTOs;

namespace SteamAPI.Models.VoteDTOs
{
    public class VoteBaseDTO
    {
        public int VoteId { get; set; }
        public double Score { get; set; }
    }
}
