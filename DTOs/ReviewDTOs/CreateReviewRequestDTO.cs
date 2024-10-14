using InfluencersPlatformBackend.Models;

namespace InfluencersPlatformBackend.DTOs.ReviewDTOs
{
    public class CreateReviewRequestDTO
    {
        public int InfluencerId { get; set; } //of a user, not a profile
        public int CompanyId { get; set; } //of a user, not a profile
        public string? Name { get; set; }
        public string Description { get; set; }
        public int? Stars { get; set; }
        public bool IsAboutInfluencer { get; set; } //is this review from a company about 
        //a influencer or vice versa?
    }
}
