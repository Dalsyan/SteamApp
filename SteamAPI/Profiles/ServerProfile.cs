using AutoMapper;
using SteamAPI.Models.ServerDTOs;

namespace SteamAPI.Profiles
{
    public class ServerProfile : Profile
    {
        public ServerProfile()
        {
            CreateMap<SteamDomain.Server, ServerDTO>();
            CreateMap<ServerForCreationDTO, SteamDomain.Server>();
            CreateMap<ServerForUpdateDTO, SteamDomain.Server>();
            CreateMap<SteamDomain.Server, ServerBaseDTO>();
        }
    }
}
