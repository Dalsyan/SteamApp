using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using SteamAPI.Models.DeveloperDTOs;
using SteamAPI.Models.GameDTOs;
using SteamAPI.Services;
using SteamData;
using SteamDomain;

namespace SteamAPI.Controllers
{
    [Route("api/developers")]
    [ApiController]
    public class DevelopersController : ControllerBase
    {
        private readonly ISteamRepository _steamRepo;
        private readonly IMapper _mapper;

        public DevelopersController(ISteamRepository steamRepository, IMapper mapper)
        {
            _steamRepo = steamRepository ??
                throw new ArgumentNullException(nameof(steamRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/developers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeveloperDTO>>> GetDevelopers()
        {
            var developers = await _steamRepo.GetAllDevelopersAsync();
            return Ok(_mapper.Map<IEnumerable<DeveloperDTO>>(developers));
        }

        // GET: api/developers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DeveloperDTO>> GetDeveloper(int id)
        {
            var developer = await _steamRepo.GetDeveloperAsync(id);
            if (developer == null) 
            {
                return NotFound();
            }

            return Ok(_mapper.Map<DeveloperDTO>(developer));
        }

        // PUT: api/developers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutDeveloper(int id, DeveloperForUpdateDTO developerDTO)
        {
            if (!await _steamRepo.DeveloperExistsAsync(id))
            {
                return NotFound();
            }

            var developer = await _steamRepo.GetContext().Devs.AsTracking().FirstOrDefaultAsync(d => d.DevId == id);

            _mapper.Map(developerDTO, developer);
            await _steamRepo.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/developers/5/games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/games")]
        public async Task<ActionResult> PutDeveloperGame(int id, GameForCreationDTO game)
        {
            if (!await _steamRepo.DeveloperExistsAsync(id))
            {
                return NotFound();
            }

            var dev = await _steamRepo.GetContext().Devs.AsTracking().FirstOrDefaultAsync(d => d.DevId == id);

            var finalGame = _mapper.Map<Game>(game);

            foreach (var games in _steamRepo.GetAllGamesBaseAsync().Result)
            {
                if (games.Title == game.Title)
                {
                    return NoContent();
                }
            }

            dev.Games.Add(finalGame);

            await _steamRepo.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/developers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostDeveloper(DeveloperForCreationDTO developer)
        {
            var finalDeveloper = _mapper.Map<Developer>(developer);

            await _steamRepo.AddDeveloperAsync(finalDeveloper);
            await _steamRepo.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/developers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeveloper(int id)
        {
            var developer = _steamRepo.GetDeveloperAsync(id).Result;
            if (developer == null)
            {
                return NotFound();
            }

            await _steamRepo.DeleteDeveloperAsync(developer);
            await _steamRepo.SaveChangesAsync();
            return NoContent();
        }
    }
}
