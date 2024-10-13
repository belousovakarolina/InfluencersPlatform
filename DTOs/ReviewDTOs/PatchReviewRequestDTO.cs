using InfluencersPlatformBackend.Models;

namespace InfluencersPlatformBackend.DTOs.ReviewDTOs
{
    public class PatchReviewRequestDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Stars { get; set; }
        public bool? Verified { get; set; }
    }
}
