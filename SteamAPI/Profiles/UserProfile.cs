using AutoMapper;

namespace SteamAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<SteamDomain.User, Models.UserDTO>();
        }
    }
}
