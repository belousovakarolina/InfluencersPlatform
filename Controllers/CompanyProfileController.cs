using InfluencersPlatformBackend.Data;
using InfluencersPlatformBackend.DTOs.CompanyProfileDTOs;
using InfluencersPlatformBackend.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InfluencersPlatformBackend.Controllers
{
    [Route("api/v1/companyprofile")]
    [ApiController]
    public class CompanyProfileController: ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public CompanyProfileController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyProfile([FromRoute] int id)
        {
            var CompanyProfile = await _context.CompanyProfiles.FindAsync(id);
            if (CompanyProfile == null)
                return NotFound();

            return Ok(CompanyProfile.ToCompanyProfileDTO());
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanyProfileList()
        {
            var CompanyProfiles = await _context.CompanyProfiles
                .Select(s => s.ToCompanyProfileDTO()).ToListAsync();

            if (CompanyProfiles == null)
                return NotFound();

            return Ok(CompanyProfiles);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompanyProfile([FromBody] CreateCompanyProfileRequestDTO newCompanyProfileRequest)
        {
            var CompanyProfile = newCompanyProfileRequest.FromCreateCompanyProfileRequestToCompanyProfile();
            _context.CompanyProfiles.Add(CompanyProfile);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCompanyProfile), new { id = CompanyProfile.Id }, CompanyProfile.ToCompanyProfileDTO());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWholeCompanyProfile([FromRoute] int id, [FromBody] PutCompanyProfileRequestDTO CompanyProfileDTO)
        {
            var CompanyProfile = _context.CompanyProfiles.FirstOrDefault(c => c.Id == id);

            if (CompanyProfile == null) return NotFound();

            CompanyProfile = CompanyProfileDTO.FromPutCompanyProfileRequestToCompanyProfile(CompanyProfile);
            await _context.SaveChangesAsync();
            return Ok(CompanyProfile.ToCompanyProfileDTO());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompanyProfile([FromRoute] int id)
        {
            var CompanyProfile = _context.CompanyProfiles.FirstOrDefault(c => c.Id == id);

            if (CompanyProfile == null) return NotFound();

            _context.CompanyProfiles.Remove(CompanyProfile);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
