using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SteamAPI.Models;
using SteamAPI.Services;
using SteamData;
using SteamDomain;

namespace SteamAPI.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ISteamRepository _steamRepo;
        private readonly IMapper _mapper;

        public AccountsController(ISteamRepository steamRepository, IMapper mapper)
        {
            _steamRepo = steamRepository ??
                throw new ArgumentNullException(nameof(steamRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountDTO>>> GetAccounts()
        {
            var accounts = await _steamRepo.GetAllAccountsAsync();
            return Ok(_mapper.Map<IEnumerable<AccountDTO>>(accounts));
        }

        // GET: api/accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDTO>> GetAccount(int id)
        {
            var account = await _steamRepo.GetAccountAsync(id);
            if (account == null) 
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AccountDTO>(account));
        }

        // PUT: api/accounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAccount(int id, AccountForUpdateDTO accountDTO)
        {
            if (!await _steamRepo.AccountExistsAsync(id))
            {
                return NotFound();
            }

            var account = await _steamRepo.GetContext().Accounts.AsTracking().FirstOrDefaultAsync(a => a.EmailId == id);

            _mapper.Map(accountDTO, account);
            await _steamRepo.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostAccount(AccountForCreationDTO account)
        {
            var finalAccount = _mapper.Map<Account>(account);

            await _steamRepo.AddAccountAsync(finalAccount);
            await _steamRepo.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var account = _steamRepo.GetAccountAsync(id).Result;
            if (account == null)
            {
                return NotFound();
            }

            await _steamRepo.DeleteAccountAsync(account);
            await _steamRepo.SaveChangesAsync();
            return NoContent();
        }
    }
}
