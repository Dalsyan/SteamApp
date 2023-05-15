using Microsoft.EntityFrameworkCore;
using SteamAPI;
using SteamAPI.Models;
using SteamAPI.Models.GameDTOs;
using SteamAPI.Services;
using SteamData;
using SteamDomain;
using System.Diagnostics;

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
                var bizlogic = new SteamRepository(context);
                var gameRetrieved = bizlogic.GetAllGamesAsync().GetAwaiter().GetResult();
                Debug.WriteLine($"seedCount: {seedCount}");

                Assert.AreEqual(seedCount, gameRetrieved.Count());
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
                var bizlogic = new SteamRepository(context);
                var gameRetrieved = bizlogic.GetGameAsync(seededId).GetAwaiter().GetResult();
                Debug.WriteLine($"seededId: {seededId}");

                Assert.AreEqual(seededId, gameRetrieved.GameId);
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
                var game = new Game { Title = "test" };
                Debug.WriteLine($"gamesCount: {context.Games.Count()}");
                context.Games.Add(game);
                context.SaveChanges();
                Debug.WriteLine($"gamesCount: {context.Games.Count()}");

                Assert.AreEqual(context.Games.Count(), 1);
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
                var gameDTO = new GameDTO { GameId = 1, Title = "test" };
                Debug.WriteLine($"gameDTO: {gameDTO.GameId}");
                Debug.WriteLine($"gameDTO: {gameDTO.Title}");

                var bizlogic = new SteamRepository(context);
                //var canUpdate = bizlogic.UpdateGame(gameDTO).GetAwaiter().GetResult();

                //var gameRetrieved = bizlogic.GetGameById(seededId);
                //Debug.WriteLine($"gameRetrieved: {gameRetrieved.Result.GameId}");
                //Debug.WriteLine($"gameRetrieved: {gameRetrieved.Result.Title}");
                //if (canUpdate)
                //{
                //    gameRetrieved.Result.Title = gameDTO.Title;
                //    gameRetrieved.Result.Gender = gameDTO.Gender;
                //    Debug.WriteLine($"gameRetrieved: {gameRetrieved.Result.GameId}");
                //    Debug.WriteLine($"gameRetrieved: {gameRetrieved.Result.Title}");
                //}
                //context.SaveChanges();

                //Assert.AreEqual(gameRetrieved.Result.Title, gameDTO.Title);
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
                var bizlogic = new SteamRepository(context);
                Debug.WriteLine($"gamesCount: {context.Games.Count()}");
                var gameRetrieved = bizlogic.GetGameAsync(3).GetAwaiter().GetResult();
                bizlogic.DeleteGameAsync(gameRetrieved).GetAwaiter().GetResult();
                //context.SaveChanges();
                Debug.WriteLine($"gamesCount: {context.Games.Count()}");

                Assert.IsNull(context.Games.Find(3));
            }
        }

        private int SeedOneGame(DbContextOptions<SteamContext> options)
        {
            using (var seedcontext = new SteamContext(options))
            {
                var game = new Game { Title = "Dead Island" };
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
                    new Game { Title = "Dead Island" },
                    new Game { Title = "Dead Island 2" },
                    new Game { Title = "Dead Island Riptide" },
                    new Game { Title = "Dead Island 3" }
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