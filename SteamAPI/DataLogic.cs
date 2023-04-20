using Microsoft.EntityFrameworkCore;
using SteamAPI.Models;
using SteamData;
using SteamDomain;

namespace SteamAPI
{
    public class DataLogic
    {
        SteamContext _context;
        public DataLogic(SteamContext context)
        {
            _context = context;
        }
        public DataLogic()
        {
            var option = new DbContextOptions<SteamContext>();
            _context = new SteamContext(option);
        }

        public async Task<List<GameDTO>> GetAllGames()
        {
            var gameList = await _context.Games.ToListAsync();
            var gameDTOList = new List<GameDTO>();
            foreach (var game in gameList)
            {
                gameDTOList.Add(GameToDTO(game));
            }
            return gameDTOList;
        }

        public async Task<GameDTO> GetGameById(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return null;
            }
            return GameToDTO(game);
        }

        public async Task<GameDTO> SaveNewGame(GameDTO gameDTO)
        {
            var game = GameFromDTO(gameDTO);
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
            return GameToDTO(game);
        }

        public async Task<bool> UpdateGame(GameDTO gameDTO)
        {
            var game = GameFromDTO(gameDTO);
            _context.Entry(game).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Games.Any(g => g.GameId == game.GameId))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            return true;
        }

        public async Task<bool> DeleteGame(int id)
        {
            if (_context.Games == null)
            {
                return false;
            }

            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return false;
            }

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ExistsGame(int id)
        {
            var game = await _context.Games.FindAsync(id);
            return (_context.Games?.Any(e => e.GameId == id)).GetValueOrDefault();
        }

        private static GameDTO GameToDTO(Game game)
        {
            return new GameDTO
            {
                GameId = game.GameId,
                Title = game.Title,
                Gender = game.Gender
            };
        }

        private static Game GameFromDTO(GameDTO gameDTO)
        {
            return new Game
            {
                GameId = gameDTO.GameId,
                Title = gameDTO.Title,
                Gender = gameDTO.Gender
            };
        }
    }
}