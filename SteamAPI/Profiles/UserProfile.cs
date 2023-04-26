using AutoMapper;
using SteamAPI.Models.UserDTOs;

namespace SteamAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<SteamDomain.User, UserDTO>();
            CreateMap<UserForCreationDTO, SteamDomain.User>();
            CreateMap<UserForUpdateDTO, SteamDomain.User>();
            CreateMap<SteamDomain.User, UserBaseDTO>();
        }
    }
}
