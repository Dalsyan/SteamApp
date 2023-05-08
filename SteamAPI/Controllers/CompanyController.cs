using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SteamAPI.Models.CompanyDTOs;
using SteamAPI.Models.GameDTOs;
using SteamAPI.Services;
using SteamData;
using SteamDomain;

namespace SteamAPI.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class companiesController : ControllerBase
    {
        private readonly ISteamRepository _steamRepo;
        private readonly IMapper _mapper;

        public companiesController(ISteamRepository steamRepository, IMapper mapper)
        {
            _steamRepo = steamRepository ??
                throw new ArgumentNullException(nameof(steamRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        #region GET
        // GET: api/companies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyDTO>>> Getcompanies()
        {
            var companies = await _steamRepo.GetAllCompaniesAsync();
            return Ok(_mapper.Map<IEnumerable<CompanyDTO>>(companies));
        }

        [HttpGet("base")]
        public async Task<ActionResult<IEnumerable<CompanyBaseDTO>>> GetcompaniesBase()
        {
            var companies = await _steamRepo.GetAllCompaniesBaseAsync();
            return Ok(_mapper.Map<IEnumerable<CompanyBaseDTO>>(companies));
        }

        // GET: api/companies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyDTO>> GetCompany(int id)
        {
            var Company = await _steamRepo.GetCompanyAsync(id);
            if (Company == null) 
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CompanyDTO>(Company));
        }

        // GET: api/companies/5/base
        [HttpGet("{id}/base")]
        public async Task<ActionResult<CompanyBaseDTO>> GetCompanyBase(int id)
        {
            var Company = await _steamRepo.GetCompanyBaseAsync(id);
            if (Company == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CompanyBaseDTO>(Company));
        }
        #endregion

        #region PUT
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        // PUT: api/companies/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutCompany(int id, CompanyForUpdateDTO CompanyDTO)
        {
            if (!await _steamRepo.CompanyExistsAsync(id))
            {
                return NotFound();
            }

            var Company = await _steamRepo.GetContext().Companies.AsTracking().FirstOrDefaultAsync(g => g.CompanyId == id);
           
            //var Company = await _steamRepo.GetCompanyAsync(id);
            _mapper.Map(CompanyDTO, Company);
            await _steamRepo.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region POST
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        // POST: api/companies
        [HttpPost]
        public async Task<ActionResult> PostCompany(CompanyForCreationDTO Company)
        {
            var finalCompany = _mapper.Map<Company>(Company);

            await _steamRepo.AddCompanyAsync(finalCompany);
            await _steamRepo.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/companies/1/countries
        [HttpPost("{id}/countries")]
        public async Task<ActionResult> PostCompanyCountries(int id, int countryId)
        {
            if (!await _steamRepo.CompanyExistsAsync(id))
            {
                return NotFound();
            }

            var company = await _steamRepo.GetContext().Companies.AsTracking().FirstOrDefaultAsync(c => c.CompanyId == id);

            if (!await _steamRepo.CountryExistsAsync(countryId))
            {
                return NotFound();
            }

            var country = await _steamRepo.GetContext().Countries.AsTracking().FirstOrDefaultAsync(c => c.CountryId == countryId);

            await _steamRepo.AddCountryToCompany(company, country);
            await _steamRepo.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region DELETE
        // DELETE: api/companies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var Company = _steamRepo.GetCompanyAsync(id).Result;
            if (Company == null)
            {
                return NotFound();
            }

            await _steamRepo.DeleteCompanyAsync(Company);
            await _steamRepo.SaveChangesAsync();
            return NoContent();
        }
        #endregion
    }
}
