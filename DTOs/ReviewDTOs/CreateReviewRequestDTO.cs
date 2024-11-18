using InfluencersPlatformBackend.Models;

namespace InfluencersPlatformBackend.DTOs.ReviewDTOs
{
    public class CreateReviewRequestDTO
    {
        public string InfluencerId { get; set; } //of a user, not a profile
        public string CompanyId { get; set; } //of a user, not a profile
        public string? UserId { get; set; } //creator of the review
        public string? Name { get; set; }
        public string Description { get; set; }
        public int? Stars { get; set; }
    }
}
