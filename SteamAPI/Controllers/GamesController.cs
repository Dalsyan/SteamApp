using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SteamAPI.Models.AccountDTOs;
using SteamAPI.Models.CompanyDTOs;
using SteamAPI.Models.CountryDTOs;
using SteamAPI.Models.DeveloperDTOs;
using SteamAPI.Models.GameDTOs;
using SteamAPI.Models.ServerDTOs;
using SteamAPI.Models.UserDTOs;
using SteamAPI.Services;
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

        // GET: api/games/servers
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

        // GET: api/games/groupCompany
        [HttpGet("groupCompany")]
        public async Task<ActionResult<IEnumerable<IGrouping<int, GameBaseDTO>>>> GetGamesGroupByCompany()
        {
            var games = await _steamRepo.GetAllGamesAsync();
            var gamesDTOs = _mapper.Map<IEnumerable<GameBaseDTO>>(games);
            IEnumerable<IGrouping<int, GameBaseDTO>> groupGameCompany = gamesDTOs.GroupBy(g => g.CompanyId);
            return Ok(groupGameCompany);
        }

        // GET: api/games/groupGender
        [HttpGet("groupGender")]
        public async Task<ActionResult<IEnumerable<IGrouping<int, GameBaseDTO>>>> GetGamesGroupByGender()
        {
            var games = await _steamRepo.GetAllGamesAsync();
            var gamesDTOs = _mapper.Map<IEnumerable<GameBaseDTO>>(games);
            var groupGameGender = gamesDTOs.GroupBy(g => g.Gender);         // IEnumerable<IGrouping<string, GameBaseDTO>>
            return Ok(groupGameGender);
        }
        /*
        // GET: api/games/usersCount
        [HttpGet("usersCount")]
        public async Task<ActionResult<IEnumerable<IGrouping<int, GameBaseDTO>>>> GetGamesGroupByUsersCount()
        {
            var games = await _steamRepo.Algo();
            var gamesDTOs = _mapper.Map<IEnumerable<GameDTO>>(games);
            var groupGameGender = gamesDTOs.OrderBy(g => g.Users.Count).GroupBy(g => g.Users.Count);         // IEnumerable<IGrouping<string, GameBaseDTO>>
            return Ok(groupGameGender);
        }
        */
        // GET: api/games/join
        [HttpGet("join")]
        public async Task<ActionResult<IEnumerable<CountryBaseDTO>>> GetGamesJoinCompanies()
        {
            var accounts = await _steamRepo.GetAllAccountsAsync();
            var countries = await _steamRepo.GetAllCountriesAsync();

            var gameC = countries           // source
                .Join(accounts,             // target
                    c => c.CountryId,       // FK
                    a => a.CountryId,       // PK
                    (c, a) => new { Country = c, Account = a })    // project result
                .Select(x => x.Country);    // select result

            return Ok(_mapper.Map<IEnumerable<CountryBaseDTO>>(gameC));
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

        // POST: api/games/1/users
        [HttpPost("{id}/users")]
        public async Task<ActionResult> PostGameUser(int id, int userId)
        {
            await _steamRepo.Algo(id, userId);

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
