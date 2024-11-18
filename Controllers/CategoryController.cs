using InfluencersPlatformBackend.Auth;
using InfluencersPlatformBackend.Data;
using InfluencersPlatformBackend.DTOs.CategoryDTOs;
using InfluencersPlatformBackend.Mappers;
using InfluencersPlatformBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace InfluencersPlatformBackend.Controllers
{
    [Route("api/v1/category")]
    [ApiController]
    public class CategoryController: ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public CategoryController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetCategory([FromRoute] int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();

            return Ok(category.ToCategoryDTO());
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCategoryList()
        {
            var categories = await _context.Categories
                .Select(s => s.ToCategoryDTO()).ToListAsync(); 

            if (categories == null)
                return NotFound();

            return Ok(categories);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestDTO categoryDTO)
        {
            //TODO: sugalvoti kazkoki addinimo algoritma, kad pacheckintu, ar jau su tokiais skaiciais yra

            if (categoryDTO.FollowersCountTo < categoryDTO.FollowersCountFrom)
            {
                return UnprocessableEntity(new
                {
                    message = "FollowersCountTo must be greater than or equal to FollowersCountFrom."
                });
            }

            var category = categoryDTO.FromCreateCategoryRequestToCategory();
             _context.Categories.Add(category);
             await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id}, category.ToCategoryDTO());
        }

        [HttpPut("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> UpdateWholeCategory([FromRoute] int id, [FromBody] PutCategoryRequestDTO categoryDTO)
        {
            //the [Required] attribute already checked if the required attributes are present

            if (categoryDTO.FollowersCountTo < categoryDTO.FollowersCountFrom)
            {
                // Return 422 Unprocessable Entity with a custom message
                return UnprocessableEntity(new
                {
                    message = "FollowersCountTo must be greater than or equal to FollowersCountFrom."
                });
            }

            var category = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null) return NotFound();

            category = categoryDTO.FromPutCategoryRequestToCategory(category);
            await _context.SaveChangesAsync();
            return Ok(category.ToCategoryDTO());
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> UpdateCategoryPartially([FromRoute] int id, [FromBody] PatchCategoryRequestDTO patchCategoryDTO)
        {
            if (patchCategoryDTO.FollowersCountTo < patchCategoryDTO.FollowersCountFrom)
            {
                // Return 422 Unprocessable Entity with a custom message
                return UnprocessableEntity(new
                {
                    message = "FollowersCountTo must be greater than or equal to FollowersCountFrom."
                });
            }

            // Retrieve the category from the database
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            // If the category is not found, return 404 Not Found
            if (category == null) return NotFound();

            // Only update the fields that are not null in the patch request
            category = patchCategoryDTO.FromPatchCategoryRequestToCategory(category);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return the updated category
            return Ok(category.ToCategoryDTO());
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            //TODO: cannot delete category 'undefined' (probably hardcode the id)
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null) return NotFound();
           
            //TODO: if this category has influencers, move them to undefined category
            //and then delete the category I guess?

            // Check if the category has any influencers
            if (category.Influencers.Any())
            {
                // Return 409 Conflict if the category has influencers
                return Conflict(new
                {
                    message = "Cannot delete category because it contains influencers."
                });
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
