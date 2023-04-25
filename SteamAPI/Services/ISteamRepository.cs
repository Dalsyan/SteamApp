using SteamAPI.Models;
using SteamData;
using SteamDomain;

namespace SteamAPI.Services
{
    public interface ISteamRepository
    {
        Task<bool> SaveChangesAsync();
        public SteamContext GetContext();
        #region Game
        Task<IEnumerable<Game>> GetAllGamesAsync();
        Task<Game?> GetGameAsync(int gameId);
        Task<bool> GameExistsAsync(int gameId);
        Task AddGameAsync(Game game);
        Task DeleteGameAsync(Game game);
        Task<IEnumerable<Game>> GetGamesWithUsersAsync();
        // Task AddUserToGameAsync(int id, User user);
        // Task AddNewUserToGameAsync(int id, UserForCreationDTO userDTO);
        #endregion

        #region User
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserAsync(int userId);
        Task<bool> UserExistsAsync(int userId);
        Task AddUserAsync(User user);
        Task DeleteUserAsync(User user);
        #endregion

        #region Account
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task<Account?> GetAccountAsync(int emailId);
        Task<bool> AccountExistsAsync(int emailId);
        Task AddAccountAsync(Account account);
        Task DeleteAccountAsync(Account account);
        #endregion

    }
}
