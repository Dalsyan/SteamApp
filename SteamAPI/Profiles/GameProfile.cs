using AutoMapper;
using SteamAPI.Models.GameDTOs;
using SteamDomain;

namespace SteamAPI.Profiles
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<Game, GameDTO>();
            CreateMap<GameForCreationDTO, Game>();
            CreateMap<GameForUpdateDTO, Game>();
            CreateMap<Game, GameBaseDTO>();
            CreateMap<GameBaseDTO, Game>();
            CreateMap<Game, GameUsersDTO>();
            CreateMap<Score, ScoreDTO>();
            CreateMap<ScoreDTO, Score>();
        }
    }
}
