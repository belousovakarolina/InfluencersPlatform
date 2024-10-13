using InfluencersPlatformBackend.DTOs.ReviewDTOs;
using InfluencersPlatformBackend.Models;

namespace InfluencersPlatformBackend.Mappers
{
    public static class ReviewMapper
    {
        public static GetReviewRequestDTO ToReviewDTO (this Review review)
        {
            return new GetReviewRequestDTO
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

        public static Review FromCreateReviewRequestToReview (this CreateReviewRequestDTO reviewDTO)
        {
            return new Review
            {
                InfluencerId = reviewDTO.InfluencerId,
                CompanyId = reviewDTO.CompanyId,
                Name = reviewDTO.Name,
                Description = reviewDTO.Description,
                Stars = reviewDTO.Stars,
                IsAboutInfluencer=reviewDTO.IsAboutInfluencer
            };
        }

        public static Review FromPutReviewRequestToReview (this PutReviewRequestDTO reviewDTO, Review toUpdate)
        {
            toUpdate.Name = reviewDTO.Name;
            toUpdate.Description = reviewDTO.Description;
            toUpdate.Stars = reviewDTO.Stars;
            toUpdate.Verified = reviewDTO.Verified;

            return toUpdate;
        }

        public static Review FromPatchReviewReqestToReview (this PatchReviewRequestDTO reviewDTO, Review toUpdate)
        {
            if (reviewDTO.Name != null)
                toUpdate.Name = reviewDTO.Name;
            if (reviewDTO.Description != null)
                toUpdate.Description = reviewDTO.Description;
            if (reviewDTO.Stars != null)
                toUpdate.Stars = reviewDTO.Stars;
            if (reviewDTO.Verified != null)
                toUpdate.Verified = (bool)reviewDTO.Verified;

            return toUpdate;
        }
    }
}
