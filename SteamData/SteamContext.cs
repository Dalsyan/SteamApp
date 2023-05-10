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
            var companyList = new Company[]
            {
                new Company { CompanyId = 1, CompanyName = "Riot Games"},
                new Company { CompanyId = 2, CompanyName = "ConcernedApe"},
                new Company { CompanyId = 3, CompanyName = "FromSoftware"},
                new Company { CompanyId = 4, CompanyName = "Epic Games"},
                new Company { CompanyId = 5, CompanyName = "Focus Entertainment"}
            };
            var gameList = new Game[]
            {
                new Game { GameId = 1, Title = "League of Legends", Gender = "MOBA", CompanyId = 1},
                new Game { GameId = 2, Title = "Fortnite", Gender = "Shooter", CompanyId = 4},
                new Game { GameId = 3, Title = "Call of Cthulhu", Gender = "Rol", CompanyId = 5},
                new Game { GameId = 4, Title = "Stardew Valley", Gender = "Simulator", CompanyId = 2},
            };
            var countryList = new Country[]
            {
                new Country { CountryId = 1, CountryName = "Spain"},
                new Country { CountryId = 2, CountryName = "UK"},
                new Country { CountryId = 3, CountryName = "Italy"},
                new Country { CountryId = 4, CountryName = "USA"},
                new Country { CountryId = 5, CountryName = "China"}
            };
            var accountList = new Account[]
            {
                new Account { EmailId = 1, Email = "dalsyan@email.com", Password = "0001", CreationDate = new DateTime(2000, 10, 01), CountryId = 1, UserId = 1},
                new Account { EmailId = 2, Email = "tentxten@email.com", Password = "0002", CreationDate = new DateTime(2000, 04, 30), CountryId = 1, UserId = 2 },
                new Account { EmailId = 3, Email = "jamonsioo@email.com", Password = "0003", CreationDate = new DateTime(2000, 08, 25), CountryId = 1, UserId = 3 },
                new Account { EmailId = 4, Email = "enricdetu@email.com", Password = "0004", CreationDate = new DateTime(2001, 07, 15), CountryId = 1, UserId = 4 },
                new Account { EmailId = 5, Email = "reisapo@email.com", Password = "0005", CreationDate = new DateTime(2000, 12, 08), CountryId = 1, UserId = 5}
            };
            var devList = new Developer[]
            {
                new Developer { DevId = 1, FirstName = "Pau", LastName = "Vidal", CountryId = 1, CompanyId = 1 },
                new Developer { DevId = 2, FirstName = "Ana", LastName = "Pérez", CountryId = 1, CompanyId = 1 },
                new Developer { DevId = 3, FirstName = "Enric", LastName = "Puigcerver", CountryId = 1, CompanyId = 2 },
                new Developer { DevId = 4, FirstName = "Ivan", LastName = "Fullana", CountryId = 1, CompanyId = 4},
                new Developer { DevId = 5, FirstName = "Mario", LastName = "Valencia", CountryId = 3, CompanyId = 2 }
            };
            var serverList = new Server[]
            {
                new Server { ServerId = 1, ServerName = "Riot 1", GameId = 1, CompanyId = 1, CountryId = 2 },
                new Server { ServerId = 2, ServerName = "Riot 2", GameId = 1, CompanyId = 1, CountryId = 4 },
                new Server { ServerId = 3, ServerName = "Epic 1", GameId = 2, CompanyId = 4, CountryId = 4 },
                new Server { ServerId = 4, ServerName = "Stardew 1", GameId = 4, CompanyId = 2, CountryId = 4 },
            };

            modelBuilder.Entity<User>().HasData(userList);
            modelBuilder.Entity<User>().HasIndex(u => u.Nickname).IsUnique();

            modelBuilder.Entity<Company>().HasData(companyList);
            modelBuilder.Entity<Company>().HasIndex(c => c.CompanyName).IsUnique();

            modelBuilder.Entity<Game>().HasData(gameList);
            modelBuilder.Entity<Game>().HasIndex(g => g.Title).IsUnique();

            modelBuilder.Entity<Country>().HasData(countryList);
            modelBuilder.Entity<Country>().HasIndex(c => c.CountryName).IsUnique();

            modelBuilder.Entity<Account>().HasData(accountList);
            modelBuilder.Entity<Account>().HasIndex(a => a.Email).IsUnique();

            modelBuilder.Entity<Developer>().HasData(devList);
                        
            modelBuilder.Entity<Server>().HasData(serverList);
            modelBuilder.Entity<Server>().HasIndex(s => s.ServerName).IsUnique();
        }
    }
}