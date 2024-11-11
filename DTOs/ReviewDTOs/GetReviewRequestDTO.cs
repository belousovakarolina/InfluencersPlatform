using InfluencersPlatformBackend.Models;

namespace InfluencersPlatformBackend.DTOs.ReviewDTOs
{
    public class GetReviewRequestDTO
    {
        public int Id { get; set; }
        public string InfluencerId { get; set; } //of a user, not a profile
        public User? Influencer { get; set; }
        public string CompanyId { get; set; } //of a user, not a profile
        public User? Company { get; set; }
        public string UserId { get; set; } //creator of the review
        public User? User { get; set; }
        public string? Name { get; set; }
        public string Description { get; set; }
        public int? Stars { get; set; }
        public bool Verified { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public GetReviewRequestDTO() { }
    }
}
