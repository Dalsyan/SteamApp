using AutoMapper;
using SteamAPI.Models.DeveloperDTOs;

namespace SteamAPI.Profiles
{
    public class DeveloperProfile : Profile
    {
        public DeveloperProfile()
        {
            CreateMap<SteamDomain.Developer, DeveloperDTO>();
            CreateMap<DeveloperForCreationDTO, SteamDomain.Developer>();
            CreateMap<DeveloperForUpdateDTO, SteamDomain.Developer>();
            CreateMap<SteamDomain.Developer, DeveloperBaseDTO>();
            CreateMap < DeveloperBaseDTO, SteamDomain.Developer>();
        }
    }
}
