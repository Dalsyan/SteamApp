using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SteamAPI.Models.AccountDTOs;
using SteamAPI.Models.CompanyDTOs;
using SteamAPI.Models.CountryDTOs;
using SteamAPI.Models.DeveloperDTOs;
using SteamAPI.Models.ServerDTOs;
using SteamAPI.Services;
using SteamData;
using SteamDomain;

namespace SteamAPI.Controllers
{
    [Route("api/countries")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ISteamRepository _steamRepo;
        private readonly IMapper _mapper;

        public CountriesController(ISteamRepository steamRepository, IMapper mapper)
        {
            _steamRepo = steamRepository ??
                throw new ArgumentNullException(nameof(steamRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        #region GET
        // GET: api/countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryDTO>>> GetCountrys()
        {
            var countries = await _steamRepo.GetAllCountriesAsync();
            return Ok(_mapper.Map<IEnumerable<CountryDTO>>(countries));
        }
        
        // GET: api/countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDTO>> GetCountry(int id)
        {
            var country = await _steamRepo.GetCountryAsync(id);
            if (country == null) 
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CountryDTO>(country));
        }

        // GET: api/countries/base
        [HttpGet("base")]
        public async Task<ActionResult<IEnumerable<CountryBaseDTO>>> GetCountriesBase()
        {
            var countries = await _steamRepo.GetAllCountriesBaseAsync();
            return Ok(_mapper.Map<IEnumerable<CountryBaseDTO>>(countries));
        }

        // GET: api/countries/5/base
        [HttpGet("{id}/base")]
        public async Task<ActionResult<CountryBaseDTO>> GetCountryBase(int id)
        {
            var country = await _steamRepo.GetCountryBaseAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CountryBaseDTO>(country));
        }

        // GET: api/countries/5/accounts
        [HttpGet("{id}/accounts")]
        public async Task<ActionResult<IEnumerable<AccountBaseDTO>>> GetCountryAccounts(int id)
        {
            var accounts = await _steamRepo.GetCountryAccountsAsync(id);
            return Ok(_mapper.Map<IEnumerable<AccountBaseDTO>>(accounts));
        }

        // GET: api/countries/5/companies
        [HttpGet("{id}/companies")]
        public async Task<ActionResult<IEnumerable<CompanyBaseDTO>>> GetCountryCompanies(int id)
        {
            var companies = await _steamRepo.GetCountryCompaniesAsync(id);
            return Ok(_mapper.Map<IEnumerable<AccountBaseDTO>>(companies));
        }

        // GET: api/countries/5/servers
        [HttpGet("{id}/servers")]
        public async Task<ActionResult<IEnumerable<ServerBaseDTO>>> GetCountrServers(int id)
        {
            var servers = await _steamRepo.GetCountryServersAsync(id);
            return Ok(_mapper.Map<IEnumerable<ServerBaseDTO>>(servers));
        }

        // GET: api/countries/5/devs
        [HttpGet("{id}/devs")]
        public async Task<ActionResult<IEnumerable<DeveloperBaseDTO>>> GetCountrDevs(int id)
        {
            var devs = await _steamRepo.GetCountryDevsAsync(id);
            return Ok(_mapper.Map<IEnumerable<DeveloperBaseDTO>>(devs));
        }
        #endregion

        #region PUT
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        // PUT: api/countries/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutCountry(int id, CountryForUpdateDTO countryDTO)
        {
            if (!await _steamRepo.CountryExistsAsync(id))
            {
                return NotFound();
            }

            var country = await _steamRepo.GetContext().Countries.AsTracking().FirstOrDefaultAsync(a => a.CountryId == id);

            _mapper.Map(countryDTO, country);
            await _steamRepo.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region POST
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        // POST: api/countries
        [HttpPost]
        public async Task<ActionResult> PostCountry(CountryForCreationDTO country)
        {
            var finalCountry = _mapper.Map<Country>(country);

            await _steamRepo.AddCountryAsync(finalCountry);
            await _steamRepo.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region DELETE
        // DELETE: api/countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = _steamRepo.GetCountryAsync(id).Result;
            if (country == null)
            {
                return NotFound();
            }

            await _steamRepo.DeleteCountryAsync(country);
            await _steamRepo.SaveChangesAsync();
            return NoContent();
        }
        #endregion
    }
}
