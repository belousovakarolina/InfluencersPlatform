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
            //TODO: ar yra useris su tokiu user id?
            //TODO: ar useris su tokiu user id turi role influencer?
            var User = await _context.Users.FindAsync(newCompanyProfileRequest.UserId);
            if (User == null)
            { 
                return NotFound(new
                {
                    message = "User not found."
                });
            }

            if (User.Role != "Company")
            {
                // Return 422 Unprocessable Entity with a custom message
                return UnprocessableEntity(new
                {
                    message = "User must have a permission to create Company Profile."
                });
            }
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

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCompanyProfilePartially([FromRoute] int id, [FromBody] PatchCompanyProfileRequestDTO patchCompanyProfileDTO)
        {
            // Retrieve the CompanyProfile from the database
            var CompanyProfile = await _context.CompanyProfiles.FirstOrDefaultAsync(c => c.Id == id);

            // If the CompanyProfile is not found, return 404 Not Found
            if (CompanyProfile == null) return NotFound();

            // Only update the fields that are not null in the patch request
            CompanyProfile = patchCompanyProfileDTO.FromPatchCompanyProfileRequestToCompanyProfile(CompanyProfile);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return the updated CompanyProfile
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
