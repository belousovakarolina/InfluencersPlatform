using InfluencersPlatformBackend.DTOs;
using InfluencersPlatformBackend.Models;

namespace InfluencersPlatformBackend.Mappers
{
    public static class InfluencerProfileMapper
    {
        public static InfluencerProfileDTO ToInfluencerProfileDTO(this InfluencerProfile profile)
        {
            return new InfluencerProfileDTO
            {
                Id = profile.Id,
                UserId = profile.UserId,
                User = profile.User,
                CategoryId = profile.CategoryId,
                Category = profile.Category,
                Name = profile.Name,
                Description = profile.Description,
                IgFollowerCount = profile.IgFollowerCount,
                FbFollowerCount = profile.FbFollowerCount,
                TiktokFollowerCount = profile.TiktokFollowerCount,
                Reviews = profile.Reviews
            };
        }
    }
}
