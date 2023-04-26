using AutoMapper;
using SteamAPI.Models.GameDTOs;

namespace SteamAPI.Profiles
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<SteamDomain.Game, GameDTO>();
            CreateMap<GameForCreationDTO, SteamDomain.Game>();
            CreateMap<GameForUpdateDTO, SteamDomain.Game>();
            CreateMap<SteamDomain.Game, GameBaseDTO>();
        }
    }
}
