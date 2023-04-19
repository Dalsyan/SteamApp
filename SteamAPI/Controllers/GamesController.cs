using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SteamAPI.Models;
using SteamData;
using SteamDomain;

namespace SteamAPI.Controllers
{
    [Route("api/games")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly DataLogic _dl;

        public GamesController(DataLogic dl)
        {
            _dl = dl;
        }

        // GET: api/games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDTO>>> GetGames()
        {
            var userDTOList = await _dl.GetAllGames();
            return userDTOList;
        }

        // GET: api/games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameDTO>> GetGame(int id)
        {
            var gameDTO = await _dl.GetGameById(id);
            
            if (gameDTO == null)
            {
                return NotFound();
            }

            return gameDTO;
        }

        // PUT: api/games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, GameDTO gameDTO)
        {
            if (id != gameDTO.GameId)
            {
                return BadRequest();
            }

            bool response = await _dl.UpdateGame(gameDTO);
            if (response == false)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(GameDTO gameDTO)
        {
            GameDTO nGameDto = await _dl.SaveNewGame(gameDTO);
            return CreatedAtAction("GetGame", new {id = gameDTO.GameId}, nGameDto);
        }

        // DELETE: api/games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            if (await _dl.DeleteGame(id))
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        private async Task<IActionResult> GameExists(int id)
        {
            return (IActionResult) _dl.ExistsGame(id);
        }
    }
}
