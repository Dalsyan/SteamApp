using AutoMapper;
using SteamAPI.Models.VoteDTOs;
using SteamDomain;

namespace SteamAPI.Profiles
{
    public class VoteProfile : Profile
    {
        public VoteProfile()
        {
            CreateMap<Vote, VoteDTO>();
            CreateMap<VoteForCreationDTO, Vote>();
            CreateMap<VoteForUpdateDTO, Vote>();
            CreateMap<Vote, VoteBaseDTO>();
            CreateMap<VoteBaseDTO, Vote>();
        }
    }
}
