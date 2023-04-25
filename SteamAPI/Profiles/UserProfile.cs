using AutoMapper;

namespace SteamAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<SteamDomain.User, Models.UserDTO>();
            CreateMap<Models.UserForCreationDTO, SteamDomain.User>();
            CreateMap<Models.UserForUpdateDTO, SteamDomain.User>();
            CreateMap<SteamDomain.User, Models.UserBaseDTO>();
        }
    }
}
