using AutoMapper;
using SteamAPI.Models.GenreDTOs;
using SteamDomain;

namespace SteamAPI.Profiles
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<Genre, GenreDTO>();
            CreateMap<GenreForCreationDTO, Genre>();
            CreateMap<GenreForUpdateDTO, Genre>();
            CreateMap<Genre, GenreBaseDTO>();
            CreateMap<GenreBaseDTO, Genre>();
        }
    }
}
