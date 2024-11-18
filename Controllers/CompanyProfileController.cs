using InfluencersPlatformBackend.Auth;
using InfluencersPlatformBackend.Data;
using InfluencersPlatformBackend.DTOs.CompanyProfileDTOs;
using InfluencersPlatformBackend.Mappers;
using InfluencersPlatformBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
        [Authorize]
        public async Task<IActionResult> GetCompanyProfile([FromRoute] int id)
        {
            var CompanyProfile = await _context.CompanyProfiles.FindAsync(id);
            if (CompanyProfile == null)
                return NotFound();

            return Ok(CompanyProfile.ToCompanyProfileDTO());
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCompanyProfileList()
        {
            var CompanyProfiles = await _context.CompanyProfiles
                .Select(s => s.ToCompanyProfileDTO()).ToListAsync();

            if (CompanyProfiles == null)
                return NotFound();

            return Ok(CompanyProfiles);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Company)]
        public async Task<IActionResult> CreateCompanyProfile(UserManager<User> userManager, [FromBody] CreateCompanyProfileRequestDTO newCompanyProfileRequest)
        {
            string userId = this.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);

            var user = await userManager.FindByIdAsync(userId);

            if (user.CompanyProfileId != null)
            {
                return UnprocessableEntity(new
                {
                    message = "User already has a Company Profile."
                });
            }
            var CompanyProfile = newCompanyProfileRequest.FromCreateCompanyProfileRequestToCompanyProfile(userId);
            _context.CompanyProfiles.Add(CompanyProfile);
            await _context.SaveChangesAsync();

            user.CompanyProfileId = CompanyProfile.Id;
            user.CompanyProfile = CompanyProfile;
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCompanyProfile), new { id = CompanyProfile.Id }, CompanyProfile.ToCompanyProfileDTO());
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateWholeCompanyProfile([FromRoute] int id, [FromBody] PutCompanyProfileRequestDTO CompanyProfileDTO)
        {
            var CompanyProfile = _context.CompanyProfiles.FirstOrDefault(c => c.Id == id);

            if (CompanyProfile == null) return NotFound();

            string userId = this.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (!this.HttpContext.User.IsInRole(UserRoles.Admin) && this.HttpContext.User.FindFirstValue(userId) != CompanyProfile.UserId)
            {
                return Forbid("You cannot edit this resource.");
            }

            CompanyProfile = CompanyProfileDTO.FromPutCompanyProfileRequestToCompanyProfile(CompanyProfile);
            await _context.SaveChangesAsync();
            return Ok(CompanyProfile.ToCompanyProfileDTO());
        }

        [HttpPatch("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateCompanyProfilePartially([FromRoute] int id, [FromBody] PatchCompanyProfileRequestDTO patchCompanyProfileDTO)
        {
            // Retrieve the CompanyProfile from the database
            var CompanyProfile = await _context.CompanyProfiles.FirstOrDefaultAsync(c => c.Id == id);

            // If the CompanyProfile is not found, return 404 Not Found
            if (CompanyProfile == null) return NotFound();

            string userId = this.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (!this.HttpContext.User.IsInRole(UserRoles.Admin) && this.HttpContext.User.FindFirstValue(userId) != CompanyProfile.UserId)
            {
                return Forbid("You cannot edit this resource.");
            }

            // Only update the fields that are not null in the patch request
            CompanyProfile = patchCompanyProfileDTO.FromPatchCompanyProfileRequestToCompanyProfile(CompanyProfile);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return the updated CompanyProfile
            return Ok(CompanyProfile.ToCompanyProfileDTO());
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCompanyProfile([FromRoute] int id)
        {
            var CompanyProfile = _context.CompanyProfiles.FirstOrDefault(c => c.Id == id);

            if (CompanyProfile == null) return NotFound();

            string userId = this.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (!this.HttpContext.User.IsInRole(UserRoles.Admin) && this.HttpContext.User.FindFirstValue(userId) != CompanyProfile.UserId)
            {
                return Forbid("You cannot delete this resource.");
            }

            var User = await _context.Users.FindAsync(CompanyProfile.UserId);

            _context.CompanyProfiles.Remove(CompanyProfile);

            if (User != null)
                User.IsDeleted = true;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
