using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SteamAPI.Models.CompanyDTOs;
using SteamAPI.Models.DeveloperDTOs;
using SteamAPI.Models.GameDTOs;
using SteamAPI.Models.ServerDTOs;
using SteamAPI.Models.UserDTOs;
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

        #region GET
        // GET: api/games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDTO>>> GetGames()
        {
            var games = await _steamRepo.GetAllGamesAsync();
            return Ok(_mapper.Map<IEnumerable<GameDTO>>(games));
        }

        // GET: api/games/base
        [HttpGet("base")]
        public async Task<ActionResult<IEnumerable<GameBaseDTO>>> GetGamesBase()
        {
            var games = await _steamRepo.GetAllGamesBaseAsync();
            return Ok(_mapper.Map<IEnumerable<GameBaseDTO>>(games));
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

        // GET: api/games/5/base
        [HttpGet("{id}/base")]
        public async Task<ActionResult<GameBaseDTO>> GetGameBase(int id)
        {
            var game = await _steamRepo.GetGameBaseAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GameBaseDTO>(game));
        }

        // GET: api/games/5/company
        [HttpGet("{id}/company")]
        public async Task<ActionResult<CompanyBaseDTO>> GetGameCompany(int id)
        {
            if (!await _steamRepo.GameExistsAsync(id))
            {
                return NotFound();
            }

            var company = await _steamRepo.GetGameCompanyAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CompanyBaseDTO>(company));
        }

        // GET: api/games/5/users
        [HttpGet("{id}/users")]
        public async Task<ActionResult<IEnumerable<UserBaseDTO>>> GetGameUsers(int id)
        {
            if (!await _steamRepo.GameExistsAsync(id))
            {
                return NotFound();
            }

            var users = await _steamRepo.GetGameUsersAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<UserBaseDTO>>(users));
        }

        // GET: api/games/5/devs
        [HttpGet("{id}/devs")]
        public async Task<ActionResult<IEnumerable<DeveloperBaseDTO>>> GetGameDevs(int id)
        {
            if (!await _steamRepo.GameExistsAsync(id))
            {
                return NotFound();
            }

            var devs = await _steamRepo.GetGameDevsAsync(id);
            if (devs == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<DeveloperBaseDTO>>(devs));
        }

        // GET: api/games/5/servers
        [HttpGet("{id}/servers")]
        public async Task<ActionResult<IEnumerable<ServerBaseDTO>>> GetGameServers(int id)
        {
            if (!await _steamRepo.GameExistsAsync(id))
            {
                return NotFound();
            }

            var servers = await _steamRepo.GetGameServersAsync(id);
            if (servers == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<ServerBaseDTO>>(servers));
        }
        #endregion

        #region PUT
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        
        // PUT: api/games/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutGame(int id, GameForUpdateDTO gameDTO)
        {
            if (!await _steamRepo.GameExistsAsync(id))
            {
                return NotFound();
            }

            var game = await _steamRepo.GetContext().Games.AsTracking().FirstOrDefaultAsync(g => g.GameId == id);
           
            _mapper.Map(gameDTO, game);
            await _steamRepo.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/games/5/users
        [HttpPut("{id}/users")]
        public async Task<ActionResult> PutGameUser(int id, UserBaseDTO userDTO)
        {
            if (!await _steamRepo.GameExistsAsync(id))
            {
                return NotFound();
            }

            var game = await _steamRepo.GetContext().Games.AsTracking().FirstOrDefaultAsync(g => g.GameId == id);

            var user = _mapper.Map<User>(userDTO);
            game.Users.Add(user);

            await _steamRepo.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/games/5/devs
        [HttpPut("{id}/devs")]
        public async Task<ActionResult> PutGameDev(int id, DeveloperBaseDTO devDTO)
        {
            if (!await _steamRepo.GameExistsAsync(id))
            {
                return NotFound();
            }

            var game = await _steamRepo.GetContext().Games.AsTracking().FirstOrDefaultAsync(g => g.GameId == id);

            var dev = _mapper.Map<Developer>(devDTO);
            game.Developers.Add(dev);

            await _steamRepo.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/games/5/servers
        [HttpPut("{id}/servers")]
        public async Task<ActionResult> PutGameServer(int id, ServerBaseDTO serverDTO)
        {
            if (!await _steamRepo.GameExistsAsync(id))
            {
                return NotFound();
            }

            var game = await _steamRepo.GetContext().Games.AsTracking().FirstOrDefaultAsync(g => g.GameId == id);

            var server = _mapper.Map<Server>(serverDTO);
            game.Servers.Add(server);

            await _steamRepo.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region POST
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        // POST: api/games
        [HttpPost]
        public async Task<ActionResult> PostGame(GameForCreationDTO game)
        {
            var finalGame = _mapper.Map<Game>(game);

            await _steamRepo.AddGameAsync(finalGame);
            await _steamRepo.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/games/5/users
        [HttpPost("{id}/users")]
        public async Task<ActionResult> PostGameUser(int id, UserForCreationDTO user)
        {
            var game = await _steamRepo.GetGameAsync(id);
            if (! await _steamRepo.GameExistsAsync(id))
            {
                return NotFound();
            }

            var finalUser = _mapper.Map<User>(user);

            await _steamRepo.AddGameUserAsync(game, finalUser);
            await _steamRepo.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/games/5/devs
        [HttpPost("{id}/devs")]
        public async Task<ActionResult> PostGameDev(int id, DeveloperForCreationDTO dev)
        {
            if (!await _steamRepo.GameExistsAsync(id))
            {
                return NotFound();
            }

            var game = await _steamRepo.GetContext().Games.AsTracking().FirstOrDefaultAsync(g => g.GameId == id);

            var finalDev = _mapper.Map<Developer>(dev);

            game.Developers.Add(finalDev);
            // await _steamRepo.AddDAsync(finalDev);
            await _steamRepo.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/games/5/servers
        [HttpPost("{id}/servers")]
        public async Task<ActionResult> PostGameServer(int id, ServerForCreationDTO server)
        {
            if (!await _steamRepo.GameExistsAsync(id))
            {
                return NotFound();
            }

            var game = await _steamRepo.GetContext().Games.AsTracking().FirstOrDefaultAsync(g => g.GameId == id);

            var finalServer = _mapper.Map<Server>(server);

            game.Servers.Add(finalServer);
            // await _steamRepo.AddGameAsync(finalServer);
            await _steamRepo.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region DELETE
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
        #endregion
    }
}
