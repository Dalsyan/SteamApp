using AutoMapper;
using SteamAPI.Models.CountryDTOs;

namespace SteamAPI.Profiles
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<SteamDomain.Country, CountryDTO>();
            CreateMap<CountryForCreationDTO, SteamDomain.Country>();
            CreateMap<CountryForUpdateDTO, SteamDomain.Country>();
            CreateMap<SteamDomain.Country, CountryBaseDTO>();
        }
    }
}
