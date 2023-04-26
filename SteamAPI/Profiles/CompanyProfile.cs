using AutoMapper;

namespace SteamAPI.Profiles
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<SteamDomain.Company, Models.CompanyDTO>();
            CreateMap<Models.CompanyForCreationDTO, SteamDomain.Company>();
            CreateMap<Models.CompanyForUpdateDTO, SteamDomain.Company>();
            CreateMap<SteamDomain.Company, Models.CompanyBaseDTO>();
        }
    }
}
