using InfluencersPlatformBackend.Data;
using InfluencersPlatformBackend.DTOs.CategoryDTOs;
using InfluencersPlatformBackend.Mappers;
using InfluencersPlatformBackend.Models;
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
        public async Task<IActionResult> GetCategory([FromRoute] int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();

            return Ok(category.ToCategoryDTO());
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoryList()
        {
            var categories = await _context.Categories
                .Select(s => s.ToCategoryDTO()).ToListAsync(); 

            if (categories == null)
                return NotFound();

            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestDTO newCategoryRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors if any
            }

            var category = newCategoryRequest.FromCreateCategoryRequestToCategory();
             _context.Categories.Add(category);
             await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id}, category.ToCategoryDTO());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWholeCategory([FromRoute] int id, [FromBody] PutCategoryRequestDTO categoryDTO)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null) return NotFound();

            category = categoryDTO.FromPutCategoryRequestToCategory(category);
            await _context.SaveChangesAsync();
            return Ok(category.ToCategoryDTO());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null) return NotFound();
            //TODO: if this category has influencers, move them to undefined category

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
