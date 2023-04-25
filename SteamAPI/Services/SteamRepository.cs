using Microsoft.EntityFrameworkCore;
using SteamAPI.Models;
using SteamData;
using SteamDomain;

namespace SteamAPI.Services
{
    public class SteamRepository : ISteamRepository
    {
        SteamContext _context;
        #region Constructors
        public SteamRepository(SteamContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public SteamRepository()
        {
            var option = new DbContextOptions<SteamContext>();
            _context = new SteamContext(option);
        }
        #endregion

        public async Task<bool> SaveChangesAsync()
        {
            var nchanges = await _context.SaveChangesAsync();
            return (nchanges >= 0);
        }

        public SteamContext GetContext()
        {
            return _context;
        }

        #region Game
        public async Task<IEnumerable<Game>> GetAllGamesAsync()
        {
            return await _context.Games.OrderBy(g => g.GameId).ToListAsync();
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
        public async Task<IEnumerable<Game>> GetGamesWithUsersAsync()
        {
            return await _context.Games.OrderBy(g => g.GameId).Include(g => g.Users).ToListAsync();
        }
        /*
        public async Task AddUserToGameAsync(int gameId, User user)
        {
            var game = await GetGameAsync(gameId);
            if (game != null)
            {
                game.Users.Add(user);
            }
            await _context.SaveChangesAsync();
        }
        public async Task AddNewUserToGameAsync(int gameId, UserForCreationDTO user)
        {

        }
        */
        #endregion

        #region User
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.OrderBy(u => u.UserId).Include(u => u.EmailId).ToListAsync();
        }
        public async Task<User?> GetUserAsync(int userId)
        {
            return await _context.Users.Include(u => u.EmailId).FirstOrDefaultAsync(u => u.UserId == userId);
        }
        public async Task<bool> UserExistsAsync(int userId)
        {
            return await _context.Users.AnyAsync(u => u.UserId == userId);
        }
        public async Task AddUserAsync(User user)
        {
            if (!GameExistsAsync(user.UserId).Result)
            {
                _context.Users.Add(user);
            }
            await _context.SaveChangesAsync();
        }
        public async Task DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Account
        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _context.Accounts.OrderBy(a => a.EmailId).Include(a => a.User).ToListAsync();
        }
        public async Task<Account?> GetAccountAsync(int emailId)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.EmailId == emailId);
        }
        public async Task<bool> AccountExistsAsync(int emailId)
        {
            return await _context.Accounts.AnyAsync(a => a.EmailId == emailId);
        }
        public async Task AddAccountAsync(Account account)
        {
            if (!AccountExistsAsync(account.EmailId).Result)
            {
                _context.Accounts.Add(account);
            }
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAccountAsync(Account account)
        {
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
