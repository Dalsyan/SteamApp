using Microsoft.EntityFrameworkCore;
using SteamData;
using SteamDomain;

namespace SteamAPI.Services
{
    public class SteamRepository : ISteamRepository
    {
        private readonly SteamContext _context;
        public SteamRepository(SteamContext context) 
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        #region Game
        public async Task<IEnumerable<Game>> GetAllGamesAsync()
        {
            return await _context.Games.OrderBy(g => g.Title).ToListAsync();
        }
        public async Task<Game?> GetGameAsync(int gameId)
        {
            return await _context.Games.FirstOrDefaultAsync(g => g.GameId == gameId);
        }
        public async Task<bool> GameExistsAsync(int gameId)
        {
            return await _context.Games.AnyAsync(g => g.GameId == gameId);
        }
        public async Task AddGameAsync(Game game)
        {
            if (!GameExistsAsync(game.GameId).Result)
            {
                _context.Games.Add(game);
            }
            await _context.SaveChangesAsync();
        }
        public async Task DeleteGameAsync(Game game)
        {
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region User
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.OrderBy(u => u.Nickname).ToListAsync();
        }
        public async Task<User?> GetUserAsync(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        }
        public async Task<bool> UserExistsAsync(int userId)
        {
            return await _context.Users.AnyAsync(u => u.UserId == userId);
        }
        #endregion
    }
}
