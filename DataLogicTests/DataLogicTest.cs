using Microsoft.EntityFrameworkCore;
using SteamAPI;
using SteamAPI.Models;
using SteamData;
using SteamDomain;

namespace DataLogicTests
{
    [TestClass]
    public class DataLogicTest
    {
        [TestMethod]
        public void GetGames()
        {
            var builder = new DbContextOptionsBuilder<SteamContext>();
            builder.UseInMemoryDatabase("GetGames");
            int seedCount = SeedManyGames(builder.Options);

            using (var context = new SteamContext(builder.Options)) 
            {
                var bizlogic = new DataLogic(context);
                var gameRetrieved = bizlogic.GetAllGames();

                Assert.AreEqual(seedCount, gameRetrieved.Result.Count);
            }
        }

        [TestMethod]
        public void GetGame()
        {
            var builder = new DbContextOptionsBuilder<SteamContext>();
            builder.UseInMemoryDatabase("GetGame");
            int seededId = SeedOneGame(builder.Options);

            using (var context = new SteamContext(builder.Options))
            {
                var bizlogic = new DataLogic(context);
                var gameRetrieved = bizlogic.GetGameById(seededId);

                Assert.AreEqual(seededId, gameRetrieved.Result.GameId);
            }
        }

        [TestMethod]
        public void PostGame()
        {
            var builder = new DbContextOptionsBuilder<SteamContext>();
            builder.UseInMemoryDatabase(
                Guid.NewGuid().ToString());

            using (var context = new SteamContext(builder.Options))
            {
                var game = new Game { Title = "test", Gender = "gTest" };
                context.Games.Add(game);

                Assert.AreEqual(EntityState.Added, context.Entry(game).State);
            }
        }
        [TestMethod]
        public void PutGame()
        {
            var builder = new DbContextOptionsBuilder<SteamContext>();
            builder.UseInMemoryDatabase(
                Guid.NewGuid().ToString());
            int seededId = SeedOneGame(builder.Options);

            using (var context = new SteamContext(builder.Options))
            {
                var gameDTO = new GameDTO { Title = "test", Gender = "gTest" };
                var bizlogic = new DataLogic(context);
                var canUpdate =  bizlogic.UpdateGame(gameDTO).GetAwaiter().GetResult();
                var gameRetrieved = bizlogic.GetGameById(gameDTO.GameId);
                if (canUpdate)
                {
                    gameRetrieved.Result.Title = gameDTO.Title;
                    gameRetrieved.Result.Gender = gameDTO.Gender;
                }

                Assert.AreEqual(gameRetrieved.Result.Title, gameDTO.Title);
            }
        }

        [TestMethod]
        public void DeleteGame()
        {
            var builder = new DbContextOptionsBuilder<SteamContext>();
            builder.UseInMemoryDatabase(
                Guid.NewGuid().ToString());
            int seedCount = SeedManyGames(builder.Options);

            using (var context = new SteamContext(builder.Options))
            {
                var bizlogic = new DataLogic(context);
                var gameRetrieved = bizlogic.DeleteGame(3);

                Assert.AreEqual(EntityState.Deleted, gameRetrieved.Status);
            }
        }

        private int SeedOneGame(DbContextOptions<SteamContext> options)
        {
            using (var seedcontext = new SteamContext(options))
            {
                var game = new Game { Title = "Dead Island", Gender = "Survival" };
                seedcontext.Games.Add(game);
                seedcontext.SaveChanges();
                return game.GameId;
            }
        }

        private int SeedManyGames(DbContextOptions<SteamContext> options)
        {
            using (var seedcontext = new SteamContext(options))
            {
                var gameList = new List<Game>()
                {
                    new Game { Title = "Dead Island", Gender = "Survival" },
                    new Game { Title = "Dead Island 2", Gender = "Survival" },
                    new Game { Title = "Dead Island Riptide", Gender = "Survival" },
                    new Game { Title = "Dead Island 3", Gender = "Survival" }
                };
                foreach (var game in gameList)
                {
                    seedcontext.Games.Add(game);
                }
                seedcontext.SaveChanges();
                return gameList.Count;
            }
        }
    }
}