using Microsoft.EntityFrameworkCore;
using SteamAPI.Models;
using SteamData;
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
                .Include(g => g.Servers)
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
        public async Task AddUserAsync(User user)
        {
            if (!UserExistsAsync(user.UserId).Result)
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
            if (!CountryExistsAsync(country.CountryId).Result)
            {
                _context.Countries.Add(country);
            }
            await _context.SaveChangesAsync();
        }
        public async Task DeleteCountryAsync(Country country)
        {
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Company
        public async Task<IEnumerable<Company>> GetAllCompaniesAsync()
        {
            return await _context.Companies
                .Include(c => c.Servers)
                .Include(c => c.Countries)
                .Include(c => c.Developers)
                .Include(c => c.Games)
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
            if (!CountryExistsAsync(company.CompanyId).Result)
            {
                _context.Companies.Add(company);
            }
            await _context.SaveChangesAsync();
        }
        public async Task DeleteCompanyAsync(Company company)
        {
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
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
            if (!ServerExistsAsync(server.ServerId).Result)
            {
                _context.Servers.Add(server);
            }
            await _context.SaveChangesAsync();
        }
        public async Task DeleteServerAsync(Server server)
        {
            _context.Servers.Remove(server);
            await _context.SaveChangesAsync();
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
        #endregion
    }
}
