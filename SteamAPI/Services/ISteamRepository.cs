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

        Task<IEnumerable<Game>> GetAllGamesBaseAsync();
        Task<Game?> GetGameBaseAsync(int gameId);
        Task<Company> GetGameCompanyAsync(int gameId);
        Task<IEnumerable<User?>> GetGameUsersAsync(int gameId);
        Task<IEnumerable<Developer?>> GetGameDevsAsync(int gameId);
        Task<IEnumerable<Server?>> GetGameServersAsync(int gameId);

        Task Algo(int gameId, int userId);
        #endregion

        #region User
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserAsync(int userId);
        Task<bool> UserExistsAsync(int userId);
        // Task AddUserAsync(User user);
        Task DeleteUserAsync(User user);

        Task<IEnumerable<User>> GetAllUsersBaseAsync();
        Task<User?> GetUserBaseAsync(int userId);
        Task<IEnumerable<Game?>> GetUserGamesAsync(int userId);
        Task<Account> GetUserAccountAsync(int userId);
        #endregion

        #region Account
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task<Account?> GetAccountAsync(int emailId);
        Task<bool> AccountExistsAsync(int emailId);
        Task AddAccountAsync(Account account);
        Task DeleteAccountAsync(Account account);

        Task<IEnumerable<Account>> GetAllAccountsBaseAsync();
        Task<Account?> GetAccountBaseAsync(int emailId);
        #endregion

        #region Country
        Task<IEnumerable<Country>> GetAllCountriesAsync();
        Task<Country?> GetCountryAsync(int countryId);
        Task<bool> CountryExistsAsync(int countryId);
        Task AddCountryAsync(Country country);
        Task DeleteCountryAsync(Country country);

        Task<IEnumerable<Country>> GetAllCountriesBaseAsync();
        Task<Country?> GetCountryBaseAsync(int countryId);
        Task<IEnumerable<Account?>> GetCountryAccountsAsync(int countryId);
        Task<IEnumerable<Company?>> GetCountryCompaniesAsync(int countryId);
        Task<IEnumerable<Server?>> GetCountryServersAsync(int countryId);
        Task<IEnumerable<Developer?>> GetCountryDevsAsync(int countryId);
        #endregion

        #region Company
        Task<IEnumerable<Company>> GetAllCompaniesAsync();
        Task<Company?> GetCompanyAsync(int companyId);
        Task<bool> CompanyExistsAsync(int companyId);
        Task AddCompanyAsync(Company company);
        Task DeleteCompanyAsync(Company company);

        Task<IEnumerable<Company>> GetAllCompaniesBaseAsync();
        Task<Company?> GetCompanyBaseAsync(int companyId);
        #endregion

        #region Server
        Task<IEnumerable<Server>> GetAllServersAsync();
        Task<Server?> GetServerAsync(int serverId);
        Task<bool> ServerExistsAsync(int serverId);
        Task AddServerAsync(Server server);
        Task DeleteServerAsync(Server server);

        Task<IEnumerable<Server>> GetAllServersBaseAsync();
        Task<Server?> GetServerBaseAsync(int serverId);
        Task<Game?> GetServerGameAsync(int serverId);
        Task<Country?> GetServerCountryAsync(int serverId);
        Task<Company?> GetServerCompanyAsync(int serverId);
        #endregion

        #region Developer
        Task<IEnumerable<Developer>> GetAllDevelopersAsync();
        Task<Developer?> GetDeveloperAsync(int devId);
        Task<bool> DeveloperExistsAsync(int devId);
        Task AddDeveloperAsync(Developer dev);
        Task DeleteDeveloperAsync(Developer dev);

        Task<IEnumerable<Developer>> GetAllDevsBaseAsync();
        Task<Developer?> GetDevBaseAsync(int devId);
        #endregion
    }
}
