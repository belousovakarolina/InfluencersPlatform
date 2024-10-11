using InfluencersPlatformBackend.Data;
using InfluencersPlatformBackend.DTOs.CategoryDTOs;
using InfluencersPlatformBackend.Mappers;
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
            var category = newCategoryRequest.FromCreateCategoryRequestToCategory();
             _context.Categories.Add(category);
             await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id}, category.ToCategoryDTO());
        }
    }
}
