using InfluencersPlatformBackend.Data;
using InfluencersPlatformBackend.DTOs.ReviewDTOs;
using InfluencersPlatformBackend.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> GetReview([FromRoute] int id)
        {
            var Review = await _context.Reviews.FindAsync(id);
            if (Review == null)
                return NotFound();

            return Ok(Review.ToReviewDTO());
        }

        [HttpGet]
        public async Task<IActionResult> GetReviewList()
        {
            var Reviews = await _context.Reviews
                .Select(s => s.ToReviewDTO()).ToListAsync();

            if (Reviews == null)
                return NotFound();

            return Ok(Reviews);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewRequestDTO newReviewRequest)
        {
            var Review = newReviewRequest.FromCreateReviewRequestToReview();
            _context.Reviews.Add(Review);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetReview), new { id = Review.Id }, Review.ToReviewDTO());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWholeReview([FromRoute] int id, [FromBody] PutReviewRequestDTO ReviewDTO)
        {
            var Review = _context.Reviews.FirstOrDefault(c => c.Id == id);

            if (Review == null) return NotFound();

            Review = ReviewDTO.FromPutReviewRequestToReview(Review);
            await _context.SaveChangesAsync();
            return Ok(Review.ToReviewDTO());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview([FromRoute] int id)
        {
            var Review = _context.Reviews.FirstOrDefault(c => c.Id == id);

            if (Review == null) return NotFound();

            _context.Reviews.Remove(Review);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
