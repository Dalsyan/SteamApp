using AutoMapper;

namespace SteamAPI.Profiles
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<SteamDomain.Country, Models.CountryDTO>();
            CreateMap<Models.CountryForCreationDTO, SteamDomain.Country>();
            CreateMap<Models.CountryForUpdateDTO, SteamDomain.Country>();
            CreateMap<SteamDomain.Country, Models.CountryBaseDTO>();
        }
    }
}
