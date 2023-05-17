﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SteamAPI.Models.AccountDTOs;
using SteamAPI.Models.GameDTOs;
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

        #region GET
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

        // GET: api/users/5/games
        [HttpGet("{id}/games")]
        public async Task<ActionResult<IEnumerable<VoteBaseDTO>>> GetUserGames(int id)
        {
            if (!await _steamRepo.UserExistsAsync(id))
            {
                return NotFound();
            }

            var gameList = await _steamRepo.GetUserGamesAsync(id);

            if (gameList == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<VoteBaseDTO>>(gameList));
        }

        // GET: api/users/5/account
        [HttpGet("{id}/account")]
        public async Task<ActionResult<AccountBaseDTO>> GetUserAccount(int id)
        {
            if (!await _steamRepo.UserExistsAsync(id))
            {
                return NotFound();
            }

            var account = await _steamRepo.GetAccountAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AccountBaseDTO>(account));
        }
        #endregion

        #region PUT
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

            await _steamRepo.AddGameToUser(user, game);
            await _steamRepo.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region POST
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        /* POST: api/users
        // No hay llamada POST porque no tiene sentido crear un usuario sin que exista su cuenta
        // Primero creas la cuenta, y por tanto el usuario

        // POST: api/users
        
        [HttpPost]
        public async Task<ActionResult> PostUser(UserForCreationDTO user)
        {
            var finalUser = _mapper.Map<User>(user);

            await _steamRepo.AddUserAsync(finalUser);
            await _steamRepo.SaveChangesAsync();

            return NoContent();
        }
        */

        // POST: api/users/1/games
        [HttpPost("{id}/games")]
        public async Task<ActionResult> PostUserGame(int id, int gameId)
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

            if (game.HasOnline && !user.Account.Premium)
            {
                return BadRequest("Para añadir un juego Online a un usuario, este debe ser Premium");
            }

            await _steamRepo.AddGameToUser(user, game);
            await _steamRepo.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/users/1/gameScore
        [HttpPost("{id}/gameScore")]
        public async Task<ActionResult> PostUserGameScore(int id, int gameId, double score)
        {
            if (!await _steamRepo.UserExistsAsync(id))
            {
                return NotFound();
            }
            var user = await _steamRepo.GetContext().Users
                .Include(u => u.Account)
                .Include(u => u.Votes)
                .Include(u => u.Games)
                .AsTracking().FirstOrDefaultAsync(u => u.UserId == id);

            if (!await _steamRepo.GameExistsAsync(gameId))
            {
                return NotFound();
            }
            var game = await _steamRepo.GetContext().Games
                .Include(g => g.Votes)
                .Include(g => g.Users)
                .AsTracking().FirstOrDefaultAsync(g => g.GameId == gameId);

            var gVotes = game.Votes.ToList();
            var uVotes = user.Votes.ToList();

            var algo = user.Games.FirstOrDefault(g => g.GameId == gameId);
            if (algo == null) { return BadRequest("El usuario ha de tener el juego."); }

            foreach (var gV in gVotes)
            {
                foreach (var uV in uVotes)
                {
                    if (gV.VoteId == uV.VoteId) { return BadRequest("El usuario ya ha votado este juego."); }
                }
            }

            Vote fScore = new Vote { Score = score };
            fScore.Games.Add(game);
            fScore.Users.Add(user);

            await _steamRepo.AddUserVoteAsync(user, game, fScore);
            await _steamRepo.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region DELETE
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
        #endregion
    }
}
