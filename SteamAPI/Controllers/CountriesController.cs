using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SteamAPI.Models.CountryDTOs;
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

        // PUT: api/countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        // POST: api/countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostCountry(CountryForCreationDTO country)
        {
            var finalCountry = _mapper.Map<Country>(country);

            await _steamRepo.AddCountryAsync(finalCountry);
            await _steamRepo.SaveChangesAsync();

            return NoContent();
        }

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
    }
}
