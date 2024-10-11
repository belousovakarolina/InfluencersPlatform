using InfluencersPlatformBackend.DTOs.ReviewDTOs;
using InfluencersPlatformBackend.Models;

namespace InfluencersPlatformBackend.Mappers
{
    public static class ReviewMapper
    {
        public static ReviewDTO ToReviewDTO (this Review review)
        {
            return new ReviewDTO
            {
                Id = review.Id,
                InfluencerId = review.InfluencerId,
                Influencer = review.Influencer,
                CompanyId = review.CompanyId,
                Company = review.Company,
                Name = review.Name,
                Description = review.Description,
                Stars = review.Stars,
                Verified = review.Verified,
                IsAboutInfluencer = review.IsAboutInfluencer,
                CreatedDate = review.CreatedDate
            };
        }
    }
}
