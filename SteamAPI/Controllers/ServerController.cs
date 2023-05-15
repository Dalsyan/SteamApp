using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SteamAPI.Models.CompanyDTOs;
using SteamAPI.Models.CountryDTOs;
using SteamAPI.Models.GameDTOs;
using SteamAPI.Models.ServerDTOs;
using SteamAPI.Services;
using SteamData;
using SteamDomain;

namespace SteamAPI.Controllers
{
    [Route("api/servers")]
    [ApiController]
    public class ServersController : ControllerBase
    {
        private readonly ISteamRepository _steamRepo;
        private readonly IMapper _mapper;

        public ServersController(ISteamRepository steamRepository, IMapper mapper)
        {
            _steamRepo = steamRepository ??
                throw new ArgumentNullException(nameof(steamRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        #region GET
        // GET: api/servers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServerDTO>>> GetServers()
        {
            var servers = await _steamRepo.GetAllServersAsync();
            return Ok(_mapper.Map<IEnumerable<ServerDTO>>(servers));
        }

        // GET: api/servers/base
        [HttpGet("base")]
        public async Task<ActionResult<IEnumerable<ServerBaseDTO>>> GetServersBase()
        {
            var servers = await _steamRepo.GetAllServersBaseAsync();
            return Ok(_mapper.Map<IEnumerable<ServerBaseDTO>>(servers));
        }

        // GET: api/servers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServerDTO>> GetServer(int id)
        {
            var server = await _steamRepo.GetServerAsync(id);
            if (server == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ServerDTO>(server));
        }

        // GET: api/servers/5/base
        [HttpGet("{id}/base")]
        public async Task<ActionResult<ServerBaseDTO>> GetServerBase(int id)
        {
            var server = await _steamRepo.GetServerBaseAsync(id);
            if (server == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ServerBaseDTO>(server));
        }

        // GET: api/servers/5/game
        [HttpGet("{id}/game")]
        public async Task<ActionResult<GameBaseDTO>> GetServerGame(int id)
        {
            if (!await _steamRepo.ServerExistsAsync(id))
            {
                return NotFound();
            }

            var game = await _steamRepo.GetServerGameAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GameBaseDTO>(game));
        }

        // GET: api/servers/5/country
        [HttpGet("{id}/country")]
        public async Task<ActionResult<CountryBaseDTO>> GetServerCountry(int id)
        {
            if (!await _steamRepo.ServerExistsAsync(id)) { 
                return NotFound(); 
            }

            var country = await _steamRepo.GetServerCountryAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CountryBaseDTO>(country));
        }

        // GET: api/servers/5/company
        [HttpGet("{id}/company")]
        public async Task<ActionResult<CompanyBaseDTO>> GetServerCompany(int id)
        {
            if (!await _steamRepo.ServerExistsAsync(id))
            {
                return NotFound();
            }

            var company = await _steamRepo.GetServerCompanyAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CompanyBaseDTO>(company));
        }
        #endregion

        #region PUT
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        // PUT: api/servers/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutServer(int id, ServerForUpdateDTO ServerDTO)
        {
            if (!await _steamRepo.ServerExistsAsync(id))
            {
                return NotFound();
            }

            var Server = await _steamRepo.GetContext().Servers.AsTracking().FirstOrDefaultAsync(g => g.ServerId == id);
           
            //var Server = await _steamRepo.GetServerAsync(id);
            _mapper.Map(ServerDTO, Server);
            await _steamRepo.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region POST
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        // POST: api/servers
        [HttpPost]
        public async Task<ActionResult> PostServer(ServerForCreationDTO Server)
        {
            var fServer = _mapper.Map<Server>(Server);

            var game = await _steamRepo.GetGameAsync(Server.GameId);

            if (!game.HasOnline)
            {
                return BadRequest("Para añadir un servidor a un juego, este tiene que tener Online");
            }

            if (game.CompanyId != fServer.CompanyId)
            {
                return BadRequest("El juego del servidor ha de pertenecer a la misma compañía que el servidor");
            }

            await _steamRepo.AddServerAsync(fServer);
            await _steamRepo.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region DELETE
        // DELETE: api/servers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServer(int id)
        {
            var Server = _steamRepo.GetServerAsync(id).Result;
            if (Server == null)
            {
                return NotFound();
            }

            await _steamRepo.DeleteServerAsync(Server);
            await _steamRepo.SaveChangesAsync();
            return NoContent();
        }
        #endregion
    }
}
