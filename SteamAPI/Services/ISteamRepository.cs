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

        #region Country
        Task<IEnumerable<Country>> GetAllCountriesAsync();
        Task<Country?> GetCountryAsync(int countryId);
        Task<bool> CountryExistsAsync(int countryId);
        Task AddCountryAsync(Country country);
        Task DeleteCountryAsync(Country country);
        #endregion

        #region Company
        Task<IEnumerable<Company>> GetAllCompaniesAsync();
        Task<Company?> GetCompanyAsync(int companyId);
        Task<bool> CompanyExistsAsync(int companyId);
        Task AddCompanyAsync(Company company);
        Task DeleteCompanyAsync(Company company);
        #endregion

        #region Server
        Task<IEnumerable<Server>> GetAllServersAsync();
        Task<Server?> GetServerAsync(int serverId);
        Task<bool> ServerExistsAsync(int serverId);
        Task AddServerAsync(Server server);
        Task DeleteServerAsync(Server server);
        #endregion
    }
}
