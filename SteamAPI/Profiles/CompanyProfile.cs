using AutoMapper;
using SteamAPI.Models.CompanyDTOs;

namespace SteamAPI.Profiles
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<SteamDomain.Company, CompanyDTO>();
            CreateMap<CompanyForCreationDTO, SteamDomain.Company>();
            CreateMap<CompanyForUpdateDTO, SteamDomain.Company>();
            CreateMap<SteamDomain.Company, CompanyBaseDTO>();
        }
    }
}
