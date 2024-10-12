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
            var InfluencerProfile = newInfluencerProfileRequest.FromCreateInfluencerProfileRequestToInfluencerProfile();
            _context.InfluencerProfiles.Add(InfluencerProfile);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInfluencerProfile([FromRoute] int id)
        {
            var InfluencerProfile = _context.InfluencerProfiles.FirstOrDefault(c => c.Id == id);

            if (InfluencerProfile == null) return NotFound();

            _context.InfluencerProfiles.Remove(InfluencerProfile);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
