using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SteamAPI.Models.GameDTOs;
using SteamAPI.Services;
using SteamData;
using SteamDomain;

namespace SteamAPI.Controllers
{
    [Route("api/games")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly ISteamRepository _steamRepo;
        private readonly IMapper _mapper;

        public GamesController(ISteamRepository steamRepository, IMapper mapper)
        {
            _steamRepo = steamRepository ??
                throw new ArgumentNullException(nameof(steamRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDTO>>> GetGames()
        {
            var games = await _steamRepo.GetAllGamesAsync();
            return Ok(_mapper.Map<IEnumerable<GameDTO>>(games));
        }

        // GET: api/games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameDTO>> GetGame(int id)
        {
            var game = await _steamRepo.GetGameAsync(id);
            if (game == null) 
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GameDTO>(game));
        }

        // PUT: api/games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutGame(int id, GameForUpdateDTO gameDTO)
        {
            if (!await _steamRepo.GameExistsAsync(id))
            {
                return NotFound();
            }

            var game = await _steamRepo.GetContext().Games.AsTracking().FirstOrDefaultAsync(g => g.GameId == id);
           
            //var game = await _steamRepo.GetGameAsync(id);
            _mapper.Map(gameDTO, game);
            await _steamRepo.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}/users")]
        public async Task<ActionResult> PutUserInGame(int id, User user)
        {
            if (!await _steamRepo.GameExistsAsync(id))
            {
                return NotFound();
            }
            var game = await _steamRepo.GetContext().Games.AsTracking().FirstOrDefaultAsync(g => g.GameId == id);

            if (!await _steamRepo.UserExistsAsync(user.UserId))
            {
                await _steamRepo.AddUserAsync(user);
                game.Users.Add(user);
            }
            else
            {
                game.Users.Add(user);
            }

            await _steamRepo.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostGame(GameForCreationDTO game)
        {
            var finalGame = _mapper.Map<Game>(game);

            await _steamRepo.AddGameAsync(finalGame);
            await _steamRepo.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = _steamRepo.GetGameAsync(id).Result;
            if (game == null)
            {
                return NotFound();
            }

            await _steamRepo.DeleteGameAsync(game);
            await _steamRepo.SaveChangesAsync();
            return NoContent();
        }
    }
}
