using InfluencersPlatformBackend.Data;
using InfluencersPlatformBackend.DTOs.InfluencerProfileDTOs;
using InfluencersPlatformBackend.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InfluencersPlatformBackend.Controllers
{
    [Route("api/v1/influencerprofile")]
    [ApiController]
    public class InfluencerProfileController: ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public InfluencerProfileController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInfluencerProfile([FromRoute] int id)
        {
            var InfluencerProfile = await _context.InfluencerProfiles.FindAsync(id);
            if (InfluencerProfile == null)
                return NotFound();

            return Ok(InfluencerProfile.ToInfluencerProfileDTO());
        }

        [HttpGet]
        public async Task<IActionResult> GetInfluencerProfileList()
        {
            var InfluencerProfiles = await _context.InfluencerProfiles
                .Select(s => s.ToInfluencerProfileDTO()).ToListAsync();

            if (InfluencerProfiles == null)
                return NotFound();

            return Ok(InfluencerProfiles);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInfluencerProfile([FromBody] CreateInfluencerProfileRequestDTO newInfluencerProfileRequest)
        {
            var User = await _context.Users.FindAsync(newInfluencerProfileRequest.UserId);
            if (User == null)
            {
                return NotFound(new
                {
                    message = "User not found."
                });
            }

            if (User.Role != "Influencer")
            {
                // Return 422 Unprocessable Entity with a custom message
                return UnprocessableEntity(new
                {
                    message = "User must have a permission to create Influencer Profile."
                });
            }

            if (User.InfluencerProfileId != null)
            {
                // Return 422 Unprocessable Entity with a custom message
                return UnprocessableEntity(new
                {
                    message = "User already has an Influencer Profile."
                });
            }

            var InfluencerProfile = newInfluencerProfileRequest.FromCreateInfluencerProfileRequestToInfluencerProfile();
            _context.InfluencerProfiles.Add(InfluencerProfile);
            User.InfluencerProfile = InfluencerProfile;
            User.InfluencerProfileId = InfluencerProfile.Id;
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetInfluencerProfile), new { id = InfluencerProfile.Id }, InfluencerProfile.ToInfluencerProfileDTO());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWholeInfluencerProfile([FromRoute] int id, [FromBody] PutInfluencerProfileRequestDTO InfluencerProfileDTO)
        {
            var InfluencerProfile = _context.InfluencerProfiles.FirstOrDefault(c => c.Id == id);

            if (InfluencerProfile == null) return NotFound();

            InfluencerProfile = InfluencerProfileDTO.FromPutInfluencerProfileRequestToInfluencerProfile(InfluencerProfile);
            await _context.SaveChangesAsync();
            return Ok(InfluencerProfile.ToInfluencerProfileDTO());
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateInfluencerProfilePartially([FromRoute] int id, [FromBody] PatchInfluencerProfileRequestDTO patchInfluencerProfileDTO)
        {
            // Retrieve the InfluencerProfile from the database
            var InfluencerProfile = await _context.InfluencerProfiles.FirstOrDefaultAsync(c => c.Id == id);

            // If the InfluencerProfile is not found, return 404 Not Found
            if (InfluencerProfile == null) return NotFound();

            // Only update the fields that are not null in the patch request
            InfluencerProfile = patchInfluencerProfileDTO.FromPatchInfluencerProfileRequestToInfluencerProfile(InfluencerProfile);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return the updated InfluencerProfile
            return Ok(InfluencerProfile.ToInfluencerProfileDTO());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInfluencerProfile([FromRoute] int id)
        {
            var InfluencerProfile = _context.InfluencerProfiles.FirstOrDefault(c => c.Id == id);

            if (InfluencerProfile == null) return NotFound();

            var User = await _context.Users.FindAsync(InfluencerProfile.UserId);

            _context.InfluencerProfiles.Remove(InfluencerProfile);

            if (User != null) 
                User.IsDeleted = true;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
