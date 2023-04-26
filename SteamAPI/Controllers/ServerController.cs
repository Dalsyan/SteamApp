using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // GET: api/servers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServerDTO>>> GetServers()
        {
            var servers = await _steamRepo.GetAllServersAsync();
            return Ok(_mapper.Map<IEnumerable<ServerDTO>>(servers));
        }

        // GET: api/servers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServerDTO>> GetServer(int id)
        {
            var Server = await _steamRepo.GetServerAsync(id);
            if (Server == null) 
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ServerDTO>(Server));
        }

        // PUT: api/servers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        // POST: api/servers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostServer(ServerForCreationDTO Server)
        {
            var finalServer = _mapper.Map<Server>(Server);

            await _steamRepo.AddServerAsync(finalServer);
            await _steamRepo.SaveChangesAsync();

            return NoContent();
        }

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
    }
}
