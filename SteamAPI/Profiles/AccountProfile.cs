using AutoMapper;

namespace SteamAPI.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<SteamDomain.Account, Models.AccountDTO>();
            CreateMap<Models.AccountForCreationDTO, SteamDomain.Account>();
            CreateMap<Models.AccountForUpdateDTO, SteamDomain.Account>();
            CreateMap<SteamDomain.Account, Models.AccountBaseDTO>();
        }
    }
}
