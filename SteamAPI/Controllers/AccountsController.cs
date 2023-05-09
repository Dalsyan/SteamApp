using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SteamAPI.Models.AccountDTOs;
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

        #region GET
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

        // GET: api/accounts/base
        [HttpGet("base")]
        public async Task<ActionResult<IEnumerable<AccountBaseDTO>>> GetAccountsBase()
        {
            var accounts = await _steamRepo.GetAllAccountsBaseAsync();
            return Ok(_mapper.Map<IEnumerable<AccountBaseDTO>>(accounts));
        }

        // GET: api/accounts/5/base
        [HttpGet("{id}/base")]
        public async Task<ActionResult<AccountBaseDTO>> GetAccountBase(int id)
        {
            var account = await _steamRepo.GetAccountBaseAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AccountBaseDTO>(account));
        }

        // GET: api/accounts/accJoin
        [HttpGet("accJoin")]
        public async Task<ActionResult> GetAccountJoinCountry()
        {
            var accounts = await _steamRepo.GetAllAccountsAsync();
            var countries = await _steamRepo.GetAllCountriesAsync();

            var res = 
                accounts.Join(
                    countries,
                    c => c.CountryId,
                    a => a.CountryId,
                    (a, c) => new
                    {
                        AccounId = a.EmailId,
                        CountryId = c.CountryId
                    })
                .GroupBy(r => r.CountryId);

            foreach(var grupo in res)
            {
                foreach (var acc_pais in grupo)
                {
                    Console.WriteLine("EmailId: {0} "
                                    + "CountryId: {1} ",
                        acc_pais.AccounId,
                        acc_pais.CountryId);
                }
            }                

            return Ok();
        }

        // GET: api/accounts/accJoin2
        [HttpGet("accJoin2")]
        public async Task<ActionResult> GetAccountJoinCountry2()
        {
            var accounts = await _steamRepo.GetAllAccountsAsync();
            var countries = await _steamRepo.GetAllCountriesAsync();

            var res =
                countries.GroupJoin(
                    accounts,
                    a => a.CountryId,
                    c => c.CountryId,
                    (a, aGroup) => new
                    {
                        CountryId = a.CountryId,
                        AccountCount = aGroup.Count()
                    })
                .OrderByDescending(r => r.AccountCount);

            foreach (var acc_pais in res)
            {
                if (acc_pais.AccountCount != 0)
                {
                    Console.WriteLine("CountryId: {0} "
                                    + "AccountCount: {1} ",
                        acc_pais.CountryId,
                        acc_pais.AccountCount);
                }
            }

            return Ok();
        }
        #endregion

        #region PUT
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        // PUT: api/accounts/5
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
        #endregion

        #region POST
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        // POST: api/accounts
        [HttpPost]
        public async Task<ActionResult> PostAccount(AccountForCreationDTO account)
        {
            var finalAccount = _mapper.Map<Account>(account);

            await _steamRepo.AddAccountAsync(finalAccount);
            await _steamRepo.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region DELETE
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
        #endregion
    }
}
