using AutoMapper;
using SteamAPI.Models.AccountDTOs;

namespace SteamAPI.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<SteamDomain.Account, AccountDTO>();
            CreateMap<AccountForCreationDTO, SteamDomain.Account>();
            CreateMap<AccountForUpdateDTO, SteamDomain.Account>();
            CreateMap<SteamDomain.Account, AccountBaseDTO>();
            CreateMap<AccountBaseDTO, SteamDomain.Account>();
        }
    }
}
