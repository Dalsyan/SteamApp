using Microsoft.EntityFrameworkCore;
using SteamAPI;
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

        }

        [TestMethod]
        public void PutGame()
        {

        }

        [TestMethod]
        public void DeleteGame()
        {

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