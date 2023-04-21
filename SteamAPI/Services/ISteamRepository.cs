using SteamDomain;

namespace SteamAPI.Services
{
    public interface ISteamRepository
    {
        Task<bool> SaveChangesAsync();

        #region Game
        Task<IEnumerable<Game>> GetAllGamesAsync();
        Task<Game?> GetGameAsync(int gameId);
        Task<bool> GameExistsAsync(int gameId);
        Task AddGameAsync(Game game);
        Task DeleteGameAsync(Game game);
        #endregion

        #region User
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserAsync(int userId);
        Task<bool> UserExistsAsync(int userId);
        Task AddUserAsync(User user);
        Task DeleteUserAsync(int userId);
        #endregion
    }
}
