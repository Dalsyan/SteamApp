using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using SteamAPI.Models;
using SteamAPI.Models.AccountDTOs;
using SteamData;
using SteamData.Models;
using SteamDomain;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
            return await _context.Games.OrderBy(g => g.GameId)
                .Include(g => g.Company)
                .Include(g => g.Users)
                .Include(g => g.Developers)
                .Include(g => g.Servers).ThenInclude(s => s.Country)
                .ToListAsync();
        }
        public async Task<Game?> GetGameAsync(int gameId)
        {
            return await _context.Games
                .Include(g => g.Company)
                .Include(g => g.Users)
                .Include(g => g.Developers)
                .Include(g => g.Servers)
                .FirstOrDefaultAsync(g => g.GameId == gameId);
        }
        public async Task<bool> GameExistsAsync(int gameId)
        {
            return await _context.Games.AnyAsync(g => g.GameId == gameId);
        }
        public async Task AddGameAsync(Game game)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            var games = await _context.Games.ToListAsync();

            try
            {
                _context.Games.Add(game);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }
        }
        public async Task DeleteGameAsync(Game game)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var servers = game.Servers;

                _context.Games.Remove(game);

                await transaction.CreateSavepointAsync("game");

                foreach (var s in servers)
                {
                    _context.Servers.Remove(s);
                }

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackToSavepointAsync("game");
                await transaction.CommitAsync();
            }

        }

        public async Task<IEnumerable<Game>> GetAllGamesBaseAsync()
        {
            return await _context.Games.OrderBy(g => g.GameId)
                .ToListAsync();
        }
        public async Task<Game?> GetGameBaseAsync(int gameId)
        {
            return await _context.Games
                .FirstOrDefaultAsync(g => g.GameId == gameId);
        }
        public async Task<Company> GetGameCompanyAsync(int gameId)
        {
            var game = await _context.Games.FirstOrDefaultAsync(g => g.GameId == gameId);
            var company = await _context.Companies.Where(c => c.CompanyId == game.CompanyId).FirstOrDefaultAsync();
            return company;
        }
        public async Task<IEnumerable<User?>> GetGameUsersAsync(int gameId)
        {
            var game = await _context.Games
                .Include(g => g.Users)
                .FirstOrDefaultAsync(g => g.GameId == gameId);
            var users = game.Users.ToList();
            return users;
        }
        public async Task<IEnumerable<Developer?>> GetGameDevsAsync(int gameId)
        {
            var game = await _context.Games
                .Include(g => g.Developers)
                .FirstOrDefaultAsync(g => g.GameId == gameId);
            var devs = game.Developers.ToList();
            return devs;
        }
        public async Task<IEnumerable<Server?>> GetGameServersAsync(int gameId)
        {
            var game = await _context.Games
                .Include(g => g.Servers)
                .FirstOrDefaultAsync(g => g.GameId == gameId);
            var servers = game.Servers.ToList();
            return servers;
        }

        public async Task AddUserToGame(Game game, User user)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                game.Users.Add(user);
                user.Games.Add(game);

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }


        }
        public async Task AddDevToGame(Game game, Developer dev)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                game.Developers.Add(dev);
                dev.Games.Add(game);

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }
        }

        public async Task<IEnumerable<Game?>> GameLike(string name)
        {
            return await _context.Games
                .Where(g => g.Title.Contains(name))
                .ToListAsync();
        }
        #endregion

        #region User
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.OrderBy(u => u.UserId)
                .Include(u => u.Account)
                .Include(u => u.Games)
                .ToListAsync();
        }
        public async Task<User?> GetUserAsync(int userId)
        {
            return await _context.Users
                .Include(u => u.Account)
                .Include(u => u.Games)
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }
        public async Task<bool> UserExistsAsync(int userId)
        {
            return await _context.Users.AnyAsync(u => u.UserId == userId);
        }

        public async Task DeleteUserAsync(User user)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                _context.Users.Remove(user);

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }

        }

        public async Task<IEnumerable<User>> GetAllUsersBaseAsync()
        {
            return await _context.Users.OrderBy(g => g.UserId)
                .ToListAsync();
        }
        public async Task<User?> GetUserBaseAsync(int userId)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }
        public async Task<IEnumerable<Game?>> GetUserGamesAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Games)
                .FirstOrDefaultAsync(u => u.UserId == userId);
            var games = user.Games.ToList();
            return games;
        }
        public async Task<Account> GetUserAccountAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Account)
                .FirstOrDefaultAsync(u => u.UserId == userId);
            var account = user.Account;
            return account;
        }

        public async Task AddGameToUser(User user, Game game)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                game.Users.Add(user);
                user.Games.Add(game);

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }
        }
        #endregion

        #region Account
        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _context.Accounts.OrderBy(a => a.EmailId)
                .Include(a => a.Country)
                .Include(a => a.User)
                .ToListAsync();
        }
        public async Task<Account?> GetAccountAsync(int emailId)
        {
            return await _context.Accounts
                .Include(a => a.Country)
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.EmailId == emailId);
        }
        public async Task<bool> AccountExistsAsync(int emailId)
        {
            return await _context.Accounts.AnyAsync(a => a.EmailId == emailId);
        }
        public async Task AddAccountAsync(Account account)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                _context.Accounts.Add(account);

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }

        }
        public async Task DeleteAccountAsync(Account account)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var user = account.User;
                _context.Users.Remove(user);
                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }
        }

        public async Task<IEnumerable<Account>> GetAllAccountsBaseAsync()
        {
            return await _context.Accounts.OrderBy(a => a.EmailId)
                .ToListAsync();
        }
        public async Task<Account?> GetAccountBaseAsync(int emailId)
        {
            return await _context.Accounts
                .FirstOrDefaultAsync(a => a.EmailId == emailId);
        }
        #endregion

        #region Country
        public async Task<IEnumerable<Country>> GetAllCountriesAsync()
        {
            return await _context.Countries.OrderBy(c => c.CountryId)
                .Include(c => c.Companies)
                .Include(c => c.Servers)
                .Include(c => c.Developers)
                .Include(c => c.Accounts)
                .ToListAsync();
        }
        public async Task<Country?> GetCountryAsync(int countryId)
        {
            return await _context.Countries
                .Include(c => c.Companies)
                .Include(c => c.Servers)
                .Include(c => c.Developers)
                .Include(c => c.Accounts)
                .FirstOrDefaultAsync(c => c.CountryId == countryId);
        }
        public async Task<bool> CountryExistsAsync(int countryId)
        {
            return await _context.Countries.AnyAsync(c => c.CountryId == countryId);
        }
        public async Task AddCountryAsync(Country country)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                _context.Countries.Add(country);

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }

        }
        public async Task DeleteCountryAsync(Country country)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                _context.Countries.Remove(country);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }
        }

        public async Task<IEnumerable<Country>> GetAllCountriesBaseAsync()
        {
            return await _context.Countries.OrderBy(c => c.CountryId)
                .ToListAsync();
        }
        public async Task<Country?> GetCountryBaseAsync(int countryId)
        {
            return await _context.Countries
                .FirstOrDefaultAsync(c => c.CountryId == countryId);
        }
        public async Task<IEnumerable<Account?>> GetCountryAccountsAsync(int countryId)
        {
            var country = await _context.Countries
                .Include(g => g.Accounts)
                .FirstOrDefaultAsync(c => c.CountryId == countryId);
            var accounts = country.Accounts.ToList();
            return accounts;
        }
        public async Task<IEnumerable<Company?>> GetCountryCompaniesAsync(int countryId)
        {
            var country = await _context.Countries
                .Include(g => g.Companies)
                .FirstOrDefaultAsync(c => c.CountryId == countryId);
            var companies = country.Companies.ToList();
            return companies;
        }
        public async Task<IEnumerable<Server?>> GetCountryServersAsync(int countryId)
        {
            var country = await _context.Countries
                .Include(g => g.Servers).ThenInclude(s => s.Country)
                .FirstOrDefaultAsync(c => c.CountryId == countryId);
            var servers = country.Servers.ToList();
            return servers;
        }
        public async Task<IEnumerable<Developer?>> GetCountryDevsAsync(int countryId)
        {
            var country = await _context.Countries
                .Include(g => g.Developers)
                .FirstOrDefaultAsync(c => c.CountryId == countryId);
            var devs = country.Developers.ToList();
            return devs;
        }

        public async Task AddCompanyToCountry(Country country, Company company)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                country.Companies.Add(company);
                company.Countries.Add(country);

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }

        }
        #endregion

        #region Company
        public async Task<IEnumerable<Company>> GetAllCompaniesAsync()
        {
            return await _context.Companies
                .Include(c => c.Countries)
                .Include(c => c.Developers)
                .Include(c => c.Games)
                .Include(c => c.Servers).ThenInclude(s => s.Country)
                .OrderBy(c => c.CompanyId).ToListAsync();
        }
        public async Task<Company?> GetCompanyAsync(int companyId)
        {
            return await _context.Companies
                .Include(c => c.Servers)
                .Include(c => c.Countries)
                .Include(c => c.Developers)
                .Include(c => c.Games)
                .FirstOrDefaultAsync(c => c.CompanyId == companyId);
        }
        public async Task<bool> CompanyExistsAsync(int companyId)
        {
            return await _context.Companies.AnyAsync(c => c.CompanyId == companyId);
        }
        public async Task AddCompanyAsync(Company company)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                _context.Companies.Add(company);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }
        }
        public async Task DeleteCompanyAsync(Company company)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                _context.Companies.Remove(company);

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }
        }

        public async Task<IEnumerable<Company>> GetAllCompaniesBaseAsync()
        {
            return await _context.Companies.OrderBy(c => c.CompanyId)
                .ToListAsync();
        }
        public async Task<Company?> GetCompanyBaseAsync(int companyId)
        {
            return await _context.Companies
                .FirstOrDefaultAsync(c => c.CompanyId == companyId);
        }
        public async Task<IEnumerable<Game?>> GetCompanyGamesAsync(int companyId)
        {
            var company = await _context.Companies
                .Include(u => u.Games)
                .FirstOrDefaultAsync(c => c.CompanyId == companyId);
            var games = company.Games.ToList();
            return games;
        }
        public async Task<IEnumerable<Server?>> GetCompanyServersAsync(int companyId)
        {
            var company = await _context.Companies
                .Include(c => c.Servers).ThenInclude(s => s.Country)
                .FirstOrDefaultAsync(c => c.CompanyId == companyId);
            var servers = company.Servers.ToList();
            return servers;
        }
        public async Task<IEnumerable<Country?>> GetCompanyCountriesAsync(int companyId)
        {
            var company = await _context.Companies
                .Include(c => c.Countries)
                .FirstOrDefaultAsync(c => c.CompanyId == companyId);
            var countries = company.Countries.ToList();
            return countries;
        }
        public async Task<IEnumerable<Developer?>> GetCompanyDevsAsync(int companyId)
        {
            var company = await _context.Companies
                .Include(c => c.Developers)
                .FirstOrDefaultAsync(c => c.CompanyId == companyId);
            var devs = company.Developers.ToList();
            return devs;
        }

        public async Task AddCountryToCompany(Company company, Country country)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                country.Companies.Add(company);
                company.Countries.Add(country);

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }

        }
        #endregion

        #region Server
        public async Task<IEnumerable<Server>> GetAllServersAsync()
        {
            return await _context.Servers.OrderBy(s => s.ServerId)
                .Include(s => s.Company)
                .Include(s => s.Country)
                .Include(s => s.Game)
                .ToListAsync();
        }
        public async Task<Server?> GetServerAsync(int serverId)
        {
            return await _context.Servers
                .Include(s => s.Company)
                .Include(s => s.Country)
                .Include(s => s.Game)
                .FirstOrDefaultAsync(s => s.ServerId == serverId);
        }
        public async Task<bool> ServerExistsAsync(int serverId)
        {
            return await _context.Servers.AnyAsync(s => s.ServerId == serverId);
        }
        public async Task AddServerAsync(Server server)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            
            try
            {
                _context.Servers.Add(server);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }
        }
        public async Task DeleteServerAsync(Server server)
        {
            _context.Servers.Remove(server);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Server>> GetAllServersBaseAsync()
        {
            return await _context.Servers.OrderBy(s => s.ServerId)
                .ToListAsync();
        }
        public async Task<Server?> GetServerBaseAsync(int serverId)
        {
            return await _context.Servers
                .FirstOrDefaultAsync(s => s.ServerId == serverId);
        }
        public async Task<Game?> GetServerGameAsync(int serverId)
        {
            var server = await _context.Servers
                .Include(s => s.Game)
                .FirstOrDefaultAsync(s => s.ServerId == serverId);
            return server.Game;
        }
        public async Task<Country?> GetServerCountryAsync(int serverId)
        {
            var server = await _context.Servers
                .Include(s => s.Country)
                .FirstOrDefaultAsync(s => s.ServerId == serverId);
            return server.Country;
        }
        public async Task<Company?> GetServerCompanyAsync(int serverId)
        {
            var server = await _context.Servers
                .Include(s => s.Company)
                .FirstOrDefaultAsync(s => s.ServerId == serverId);
            return server.Company;
        }
        #endregion

        #region Developer
        public async Task<IEnumerable<Developer>> GetAllDevelopersAsync()
        {
            return await _context.Devs.OrderBy(d => d.DevId)
                .Include(d => d.Company)
                .Include(d => d.Country)
                .Include(d => d.Games)
                .ToListAsync();
        }
        public async Task<Developer?> GetDeveloperAsync(int devId)
        {
            return await _context.Devs
                .Include(d => d.Company)
                .Include(d => d.Country)
                .Include(d => d.Games)
                .FirstOrDefaultAsync(s => s.DevId == devId);
        }
        public async Task<bool> DeveloperExistsAsync(int devId)
        {
            return await _context.Devs.AnyAsync(s => s.DevId == devId);
        }
        public async Task AddDeveloperAsync(Developer dev)
        {
            if (!DeveloperExistsAsync(dev.DevId).Result)
            {
                _context.Devs.Add(dev);
            }
            await _context.SaveChangesAsync();
        }
        public async Task DeleteDeveloperAsync(Developer dev)
        {
            _context.Devs.Remove(dev);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Developer>> GetAllDevsBaseAsync()
        {
            return await _context.Devs.OrderBy(d => d.DevId)
                .ToListAsync();
        }
        public async Task<Developer?> GetDevBaseAsync(int devId)
        {
            return await _context.Devs
                .FirstOrDefaultAsync(d => d.DevId == devId);
        }

        public async Task AddGameToDev(Developer dev, Game game)
        {
            game.Developers.Add(dev);
            dev.Games.Add(game);

            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
