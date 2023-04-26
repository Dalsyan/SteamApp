using Microsoft.EntityFrameworkCore;
using SteamDomain;

namespace SteamData
{
    public class SteamContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Developer> Devs { get; set; }
        public DbSet<Server> Servers { get; set; }

        public SteamContext()
        {

        }

        public SteamContext(DbContextOptions<SteamContext> options) 
            : base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var userList = new User[]
            {
                new User { UserId = 1, Nickname = "dalsyan"},
                new User { UserId = 2, Nickname = "Tentxten"},
                new User { UserId = 3, Nickname = "Jamonsioo"},
                new User { UserId = 4, Nickname = "EnricDeTu"},
                new User { UserId = 5, Nickname = "ReiSapo"}
            };
            modelBuilder.Entity<User>().HasData(userList);

            var companyList = new Company[]
            {
                new Company { CompanyId = 1, CompanyName = "Riot Games"},
                new Company { CompanyId = 2, CompanyName = "ConcernedApe"},
                new Company { CompanyId = 3, CompanyName = "FromSoftware"},
                new Company { CompanyId = 4, CompanyName = "Epic Games"},
                new Company { CompanyId = 5, CompanyName = "Focus Entertainment"}
            };
            modelBuilder.Entity<Company>().HasData(companyList);

            var gameList = new Game[]
            {
                new Game { GameId = 1, Title = "League of Legends", Gender = "MOBA", CompanyId = 1},
                new Game { GameId = 2, Title = "Fortnite", Gender = "Shooter", CompanyId = 4},
                new Game { GameId = 3, Title = "Call of Cthulhu", Gender = "Rol", CompanyId = 5},
                new Game { GameId = 4, Title = "Stardew Valley", Gender = "Simulator", CompanyId = 2},
            };
            modelBuilder.Entity<Game>().HasData(gameList);

            var countryList = new Country[]
            {
                new Country { CountryId = 1, CountryName = "Spain"},
                new Country { CountryId = 2, CountryName = "UK"},
                new Country { CountryId = 3, CountryName = "Italy"},
                new Country { CountryId = 4, CountryName = "USA"},
                new Country { CountryId = 5, CountryName = "China"}
            };
            modelBuilder.Entity<Country>().HasData(countryList);

            var accountList = new Account[]
            {
                new Account { EmailId = 1, UserId = 1 ,Email = "dalsyan@email.com", Password = "0001", CreationDate = new DateTime(2000, 10, 01)},
                new Account { EmailId = 2, UserId = 2, Email = "tentxten@email.com", Password = "0002", CreationDate = new DateTime(2000, 04, 30) },
                new Account { EmailId = 3, UserId = 3, Email = "jamonsioo@email.com", Password = "0003", CreationDate = new DateTime(2000, 08, 25) },
                new Account { EmailId = 4, UserId = 4, Email = "enricdetu@email.com", Password = "0004", CreationDate = new DateTime(2001, 07, 15) },
                new Account { EmailId = 5, UserId = 5, Email = "reisapo@email.com", Password = "0005", CreationDate = new DateTime(2000, 12, 08)}
            };
            modelBuilder.Entity<Account>().HasData(accountList);
        }
    }
}