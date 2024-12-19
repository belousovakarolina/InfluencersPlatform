using InfluencersPlatformBackend.Auth;
using InfluencersPlatformBackend.Data;
using InfluencersPlatformBackend.DTOs.ReviewDTOs;
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
    [Route("api/v1/review")]
    [ApiController]
    public class ReviewController: ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public ReviewController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        [Authorize] 
        public async Task<IActionResult> GetReview(UserManager<User> userManager, [FromRoute] int id)
        {
            var Review = await _context.Reviews.FindAsync(id);
            if (Review == null)
                return NotFound();

            string userId = this.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            var user = await userManager.FindByIdAsync(userId);
            var roles = await userManager.GetRolesAsync(user);
            if (!roles.Contains(UserRoles.Influencer) && !roles.Contains(UserRoles.Admin))
                return Ok(Review.ToReviewDTOForInfluencer());

            return Ok(Review.ToReviewDTO());
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetReviewList(UserManager<User> userManager)
        {
            string userId = this.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            var user = await userManager.FindByIdAsync(userId);
            var roles = await userManager.GetRolesAsync(user);

            List<GetReviewRequestDTO> reviews;

            if (!roles.Contains(UserRoles.Influencer) && !roles.Contains(UserRoles.Admin))
            {
                reviews = await _context.Reviews.Select(s => s.ToReviewDTOForInfluencer()).ToListAsync();
            }
            else
            {
                reviews = await _context.Reviews.Select(s => s.ToReviewDTO()).ToListAsync();
            }

            if (reviews == null)
                return NotFound();

            return Ok(reviews);
        }

        [HttpGet("company/{id}")]
        [Authorize]
        public async Task<IActionResult> GetReviewsForCompany(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Company ID is required.");
            }
            //TODO: if user role is influencer, hide influencer information, except if it is you who left the review
            var reviews = await _context.Reviews
                .Where(r => r.CompanyId == id)
                .Select(r => r.ToReviewDTO())
                .ToListAsync();

            if (reviews == null || !reviews.Any())
            {
                return NotFound($"No reviews found for company with ID {id}.");
            }

            return Ok(reviews);
        }

        [HttpGet("influencer/{id}")]
        [Authorize]
        public async Task<IActionResult> GetReviewsForInfluencer(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Influencer ID is required.");
            }
            //TODO: if user role is influencer, hide influencer information, except if it is you who left the review
            var reviews = await _context.Reviews
                .Where(r => r.CompanyId == id)
                .Select(r => r.ToReviewDTO())
                .ToListAsync();

            if (reviews == null || !reviews.Any())
            {
                return NotFound($"No reviews found for influencer with ID {id}.");
            }

            return Ok(reviews);
        }

        [HttpPost]
        [Authorize(Roles = $"{UserRoles.Company},{UserRoles.Influencer}")] //users who only have administrator role cannot create new reviews
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewRequestDTO newReviewRequest)
        {
            string userId = this.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID could not be determined from the token.");
            }
            Console.WriteLine("userid:", userId);
            var Review = newReviewRequest.FromCreateReviewRequestToReview(userId);
            Review.Influencer = _context.Users.FirstOrDefault(c => c.Id == Review.InfluencerId);
            Review.Company = _context.Users.FirstOrDefault(c => c.Id == Review.CompanyId);
            Review.User = _context.Users.FirstOrDefault(c => c.Id == Review.UserId);
            _context.Reviews.Add(Review);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetReview), new { id = Review.Id }, Review.ToReviewDTO());
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateWholeReview([FromRoute] int id, [FromBody] PutReviewRequestDTO ReviewDTO)
        {
            var Review = _context.Reviews.FirstOrDefault(c => c.Id == id);

            if (Review == null) return NotFound();

            string userId = this.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (!this.HttpContext.User.IsInRole(UserRoles.Admin) && this.HttpContext.User.FindFirstValue(userId) != Review.UserId)
            {
                return Forbid("You cannot edit this resource.");
            }

            Review = ReviewDTO.FromPutReviewRequestToReview(Review);
            await _context.SaveChangesAsync();
            return Ok(Review.ToReviewDTO());
        }

        [HttpPatch("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateReviewPartially([FromRoute] int id, [FromBody] PatchReviewRequestDTO patchReviewDTO)
        {

            // Retrieve the Review from the database
            var Review = await _context.Reviews.FirstOrDefaultAsync(c => c.Id == id);

            // If the Review is not found, return 404 Not Found
            if (Review == null) return NotFound();

            string userId = this.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (!this.HttpContext.User.IsInRole(UserRoles.Admin) && this.HttpContext.User.FindFirstValue(userId) != Review.UserId)
            {
                return Forbid("You cannot edit this resource.");
            }

            // Only update the fields that are not null in the patch request
            Review = patchReviewDTO.FromPatchReviewReqestToReview(Review);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return the updated Review
            return Ok(Review.ToReviewDTO());
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteReview([FromRoute] int id)
        {
            var Review = _context.Reviews.FirstOrDefault(c => c.Id == id);

            if (Review == null) return NotFound();

            string userId = this.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (!this.HttpContext.User.IsInRole(UserRoles.Admin) && this.HttpContext.User.FindFirstValue(userId) != Review.UserId)
            {
                return Forbid("You cannot delete this resource.");
            }

            _context.Reviews.Remove(Review);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
