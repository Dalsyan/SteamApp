using AutoMapper;

namespace SteamAPI.Profiles
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<SteamDomain.Game, Models.GameDTO>();
            CreateMap<Models.GameForCreationDTO, SteamDomain.Game>();
            CreateMap<Models.GameForUpdateDTO, SteamDomain.Game>();
            CreateMap<SteamDomain.Game, Models.GameBaseDTO>();
        }
    }
}
