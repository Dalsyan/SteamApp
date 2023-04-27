using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SteamAPI.Models.UserDTOs;
using SteamAPI.Services;
using SteamData;
using SteamDomain;

namespace SteamAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ISteamRepository _steamRepo;
        private readonly IMapper _mapper;

        public UsersController(ISteamRepository steamRepository, IMapper mapper)
        {
            _steamRepo = steamRepository ??
                throw new ArgumentNullException(nameof(steamRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            var users = await _steamRepo.GetAllUsersAsync();
            return Ok(_mapper.Map<IEnumerable<UserDTO>>(users));
        }

        // GET: api/users/base
        [HttpGet("base")]
        public async Task<ActionResult<IEnumerable<UserBaseDTO>>> GetUsersBase()
        {
            var users = await _steamRepo.GetAllUsersBaseAsync();
            return Ok(_mapper.Map<IEnumerable<UserBaseDTO>>(users));
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            var user = await _steamRepo.GetUserAsync(id);
            if (user == null) 
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserDTO>(user));
        }

        // GET: api/users/5
        [HttpGet("{id}/base")]
        public async Task<ActionResult<UserBaseDTO>> GetUserBase(int id)
        {
            var user = await _steamRepo.GetUserBaseAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserBaseDTO>(user));
        }

        // PUT: api/users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutUser(int id, UserForUpdateDTO userDTO)
        {
            if (!await _steamRepo.UserExistsAsync(id))
            {
                return NotFound();
            }

            var user = await _steamRepo.GetContext().Users.AsTracking().FirstOrDefaultAsync(u => u.UserId == id);

            _mapper.Map(userDTO, user);
            await _steamRepo.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/users/5/games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/games")]
        public async Task<ActionResult> PutGameUser(int id, int gameId)
        {
            if (!await _steamRepo.UserExistsAsync(id))
            {
                return NotFound();
            }

            var user = await _steamRepo.GetContext().Users.AsTracking().FirstOrDefaultAsync(u => u.UserId == id);

            if (!await _steamRepo.GameExistsAsync(gameId))
            {
                return NotFound();
            }

            var game = await _steamRepo.GetContext().Games.AsTracking().FirstOrDefaultAsync(g => g.GameId == gameId);

            user.Games.Add(game);
            await _steamRepo.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostUser(UserForCreationDTO user)
        {
            var finalUser = _mapper.Map<User>(user);

            await _steamRepo.AddUserAsync(finalUser);
            await _steamRepo.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = _steamRepo.GetUserAsync(id).Result;
            if (user == null)
            {
                return NotFound();
            }

            await _steamRepo.DeleteUserAsync(user);
            await _steamRepo.SaveChangesAsync();
            return NoContent();
        }
    }
}
