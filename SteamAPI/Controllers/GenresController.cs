using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using SteamAPI.Models.GenreDTOs;
using SteamAPI.Models.GameDTOs;
using SteamAPI.Services;
using SteamData;
using SteamData.Models;
using SteamDomain;

namespace SteamAPI.Controllers
{
    [Route("api/genres")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly ISteamRepository _steamRepo;
        private readonly IMapper _mapper;

        public GenresController(ISteamRepository steamRepository, IMapper mapper)
        {
            _steamRepo = steamRepository ??
                throw new ArgumentNullException(nameof(steamRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        #region GET
        // GET: api/genres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreDTO>>> GetGenres()
        {
            var genres = await _steamRepo.GetAllGenresAsync();
            return Ok(_mapper.Map<IEnumerable<GenreDTO>>(genres));
        }

        // GET: api/genres/base
        [HttpGet("base")]
        public async Task<ActionResult<IEnumerable<GenreBaseDTO>>> GetGenresBase()
        {
            var genres = await _steamRepo.GetAllGenresAsync();
            return Ok(_mapper.Map<IEnumerable<GenreBaseDTO>>(genres));
        }

        // GET: api/genres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GenreDTO>> GetGenre(int id)
        {
            var genre = await _steamRepo.GetGenreAsync(id);
            if (genre == null) 
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GenreDTO>(genre));
        }

        [HttpGet("{id}/base")]
        public async Task<ActionResult<GenreBaseDTO>> GetGenreBase(int id)
        {
            var genre = await _steamRepo.GetGenreAsync(id);
            if (genre == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GenreBaseDTO>(genre));
        }
        #endregion

        #region PUT
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        // PUT: api/genres/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutGenre(int id, GenreForUpdateDTO developerDTO)
        {
            if (!await _steamRepo.GenreExistsAsync(id))
            {
                return NotFound();
            }

            var developer = await _steamRepo.GetContext().Devs.AsTracking().FirstOrDefaultAsync(d => d.DevId == id);

            _mapper.Map(developerDTO, developer);
            await _steamRepo.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/genres/5/games
        [HttpPut("{id}/games")]
        public async Task<ActionResult> PutGenreGame(int id, VoteForCreationDTO game)
        {
            if (!await _steamRepo.GenreExistsAsync(id))
            {
                return NotFound();
            }

            var genre = await _steamRepo.GetContext().Devs.AsTracking().FirstOrDefaultAsync(d => d.DevId == id);

            var finalGame = _mapper.Map<Game>(game);

            foreach (var games in _steamRepo.GetAllGamesBaseAsync().Result)
            {
                if (games.Title == game.Title)
                {
                    return NoContent();
                }
            }

            genre.Games.Add(finalGame);

            await _steamRepo.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region POST
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        // POST: api/genres
        [HttpPost]
        public async Task<ActionResult> PostGenre(GenreForCreationDTO genre)
        {
            var fGenre = _mapper.Map<Genre>(genre);

            await _steamRepo.AddGenreAsync(fGenre);
            await _steamRepo.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/genres/1/games
        [HttpPost("{id}/games")]
        public async Task<ActionResult> PostGenreGame(int id, int gameId)
        {
            if (!await _steamRepo.GenreExistsAsync(id))
            {
                return NotFound();
            }

            var genre = await _steamRepo.GetContext().Genres.AsTracking().FirstOrDefaultAsync(g => g.GenreId == id);

            if (!await _steamRepo.GenreExistsAsync(gameId))
            {
                return NotFound();
            }

            var game = await _steamRepo.GetContext().Games.AsTracking().FirstOrDefaultAsync(g => g.GameId == gameId);


            await _steamRepo.AddGameToGenre(genre, game);
            await _steamRepo.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region DELETE
        // DELETE: api/genres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var genre = _steamRepo.GetGenreAsync(id).Result;
            if (genre == null)
            {
                return NotFound();
            }

            await _steamRepo.DeleteGenreAsync(genre);
            await _steamRepo.SaveChangesAsync();
            return NoContent();
        }
        #endregion
    }
}
