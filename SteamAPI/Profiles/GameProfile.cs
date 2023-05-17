using AutoMapper;
using SteamAPI.Models.GameDTOs;
using SteamDomain;

namespace SteamAPI.Profiles
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<Game, VoteDTO>();
            CreateMap<VoteForCreationDTO, Game>();
            CreateMap<VoteForUpdateDTO, Game>();
            CreateMap<Game, VoteBaseDTO>();
            CreateMap<VoteBaseDTO, Game>();
            CreateMap<Game, GameUsersDTO>();
        }
    }
}
