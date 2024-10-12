using InfluencersPlatformBackend.DTOs.InfluencerProfileDTOs;
using InfluencersPlatformBackend.Models;

namespace InfluencersPlatformBackend.Mappers
{
    public static class InfluencerProfileMapper
    {
        public static GetInfluencerProfileRequestDTO ToInfluencerProfileDTO(this InfluencerProfile profile)
        {
            return new GetInfluencerProfileRequestDTO
            {
                Id = profile.Id,
                UserId = profile.UserId,
                CategoryId = profile.CategoryId,
                Name = profile.Name,
                Description = profile.Description,
                IgFollowerCount = profile.IgFollowerCount,
                FbFollowerCount = profile.FbFollowerCount,
                TiktokFollowerCount = profile.TiktokFollowerCount
            };
        }

        public static InfluencerProfile FromCreateInfluencerProfileRequestToInfluencerProfile (this CreateInfluencerProfileRequestDTO influencerDTO)
        {
            return new InfluencerProfile
            {
                UserId = influencerDTO.UserId,
                CategoryId = influencerDTO.CategoryId,
                Name = influencerDTO.Name,
                Description = influencerDTO.Description,
                IgFollowerCount = influencerDTO .IgFollowerCount,
                FbFollowerCount = influencerDTO.FbFollowerCount,
                TiktokFollowerCount = influencerDTO.TiktokFollowerCount
            };
        }

        public static InfluencerProfile FromPutInfluencerProfileRequestToInfluencerProfile (this PutInfluencerProfileRequestDTO influencerDTO, InfluencerProfile toUpdate)
        {
            toUpdate.CategoryId = influencerDTO.CategoryId;
            toUpdate.Name = influencerDTO.Name;
            toUpdate.Description = influencerDTO.Description;
            toUpdate.FbFollowerCount = toUpdate.FbFollowerCount;
            toUpdate.IgFollowerCount = toUpdate .IgFollowerCount;
            toUpdate.TiktokFollowerCount = toUpdate?.TiktokFollowerCount;

            return toUpdate;
        }
    }
}
