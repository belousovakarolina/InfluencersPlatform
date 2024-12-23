﻿using InfluencersPlatformBackend.Auth;
using InfluencersPlatformBackend.Data;
using InfluencersPlatformBackend.DTOs.InfluencerProfileDTOs;
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
        [Authorize]
        public async Task<IActionResult> GetInfluencerProfile([FromRoute] int id)
        {
            var InfluencerProfile = await _context.InfluencerProfiles.FindAsync(id);
            if (InfluencerProfile == null)
                return NotFound();

            string userId = this.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (!this.HttpContext.User.IsInRole(UserRoles.Admin) && !this.HttpContext.User.IsInRole(UserRoles.Company) && this.HttpContext.User.FindFirstValue(userId) != InfluencerProfile.UserId)
            {
                return Forbid("You cannot view this resource.");
            }

            return Ok(InfluencerProfile.ToInfluencerProfileDTO());
        }

        [HttpGet]
        [Authorize] 
        public async Task<IActionResult> GetInfluencerProfileList() 
        {
            List<GetInfluencerProfileRequestDTO> influencerProfiles;

            string userId = this.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (!HttpContext.User.IsInRole(UserRoles.Admin) && !HttpContext.User.IsInRole(UserRoles.Company))
            {
                // If the user is an Influencer, only retrieve their profile
                var influencerProfile = await _context.InfluencerProfiles
                    .Where(ip => ip.UserId == userId)
                    .Select(ip => ip.ToInfluencerProfileDTO())
                    .FirstOrDefaultAsync();

                if (influencerProfile == null)
                    return NotFound("Profile not found.");

                return Ok(new List<GetInfluencerProfileRequestDTO> { influencerProfile });
            }
            influencerProfiles = await _context.InfluencerProfiles.Select(s => s.ToInfluencerProfileDTO()).ToListAsync();
            if (influencerProfiles == null)
                return NotFound();

            return Ok(influencerProfiles);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Influencer)]
        public async Task<IActionResult> CreateInfluencerProfile(UserManager<User> userManager, [FromBody] CreateInfluencerProfileRequestDTO newInfluencerProfileRequest)
        {
            string userId = this.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);

            var user = await userManager.FindByIdAsync(userId);
            Console.WriteLine(user.UserName);

            if (user.InfluencerProfileId != null)
            {
                return UnprocessableEntity(new
                {
                    message = "User already has an Influencer Profile."
                });
            }

            var InfluencerProfile = newInfluencerProfileRequest.FromCreateInfluencerProfileRequestToInfluencerProfile(userId);
            _context.InfluencerProfiles.Add(InfluencerProfile);
            await _context.SaveChangesAsync();

            user.InfluencerProfile = InfluencerProfile;
            user.InfluencerProfileId = InfluencerProfile.Id;
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInfluencerProfile), new { id = InfluencerProfile.Id }, InfluencerProfile.ToInfluencerProfileDTO());
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateWholeInfluencerProfile([FromRoute] int id, [FromBody] PutInfluencerProfileRequestDTO InfluencerProfileDTO)
        {
            var InfluencerProfile = _context.InfluencerProfiles.FirstOrDefault(c => c.Id == id);

            if (InfluencerProfile == null) return NotFound();

            string userId = this.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (!this.HttpContext.User.IsInRole(UserRoles.Admin) && this.HttpContext.User.FindFirstValue(userId) != InfluencerProfile.UserId)
            {
                return Forbid("You cannot edit this resource.");
            }
            var newProfile = InfluencerProfileDTO.FromPutInfluencerProfileRequestToInfluencerProfile(InfluencerProfile);
            InfluencerProfile = InfluencerProfileDTO.FromPutInfluencerProfileRequestToInfluencerProfile(InfluencerProfile);
            await _context.SaveChangesAsync();
            return Ok(InfluencerProfile.ToInfluencerProfileDTO());
        }

        [HttpPatch("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateInfluencerProfilePartially([FromRoute] int id, [FromBody] PatchInfluencerProfileRequestDTO patchInfluencerProfileDTO)
        {
            // Retrieve the InfluencerProfile from the database
            var InfluencerProfile = await _context.InfluencerProfiles.FirstOrDefaultAsync(c => c.Id == id);

            // If the InfluencerProfile is not found, return 404 Not Found
            if (InfluencerProfile == null) return NotFound();

            string userId = this.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (!this.HttpContext.User.IsInRole(UserRoles.Admin) && this.HttpContext.User.FindFirstValue(userId) != InfluencerProfile.UserId)
            {
                return Forbid("You cannot edit this resource.");
            }

            // Only update the fields that are not null in the patch request
            InfluencerProfile = patchInfluencerProfileDTO.FromPatchInfluencerProfileRequestToInfluencerProfile(InfluencerProfile);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return the updated InfluencerProfile
            return Ok(InfluencerProfile.ToInfluencerProfileDTO());
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteInfluencerProfile([FromRoute] int id)
        {
            var InfluencerProfile = _context.InfluencerProfiles.FirstOrDefault(c => c.Id == id);

            if (InfluencerProfile == null) return NotFound();

            string userId = this.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (!this.HttpContext.User.IsInRole(UserRoles.Admin) && this.HttpContext.User.FindFirstValue(userId) != InfluencerProfile.UserId)
            {
                return Forbid("You cannot delete this resource.");
            }

            var User = await _context.Users.FindAsync(InfluencerProfile.UserId);

            _context.InfluencerProfiles.Remove(InfluencerProfile);

            if (User != null) 
                User.IsDeleted = true;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
