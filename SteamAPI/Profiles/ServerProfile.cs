using AutoMapper;

namespace SteamAPI.Profiles
{
    public class ServerProfile : Profile
    {
        public ServerProfile()
        {
            CreateMap<SteamDomain.Server, Models.ServerDTO>();
            CreateMap<Models.ServerForCreationDTO, SteamDomain.Server>();
            CreateMap<Models.ServerForUpdateDTO, SteamDomain.Server>();
            CreateMap<SteamDomain.Server, Models.ServerBaseDTO>();
        }
    }
}
