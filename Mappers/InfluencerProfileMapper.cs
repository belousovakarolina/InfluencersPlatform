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

        public static InfluencerProfile FromCreateInfluencerProfileRequestToInfluencerProfile (this CreateInfluencerProfileRequestDTO influencerDTO, string userId)
        {
            return new InfluencerProfile
            {
                UserId = userId,
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
            toUpdate.FbFollowerCount = influencerDTO.FbFollowerCount;
            toUpdate.IgFollowerCount = influencerDTO.IgFollowerCount;
            toUpdate.TiktokFollowerCount = influencerDTO.TiktokFollowerCount;

            return toUpdate;
        }

        public static InfluencerProfile FromPatchInfluencerProfileRequestToInfluencerProfile(this PatchInfluencerProfileRequestDTO influencerDTO, InfluencerProfile toUpdate)
        {
            if (influencerDTO.CategoryId != null)
                toUpdate.CategoryId = influencerDTO.CategoryId;
            if (influencerDTO.Name != null)
                toUpdate.Name = influencerDTO.Name;
            if (influencerDTO.Description != null)
                toUpdate.Description = influencerDTO.Description;
            if (influencerDTO.FbFollowerCount != null)
                toUpdate.FbFollowerCount = influencerDTO.FbFollowerCount;
            if (influencerDTO.IgFollowerCount != null)
                toUpdate.IgFollowerCount = influencerDTO?.IgFollowerCount;
            if (influencerDTO.TiktokFollowerCount != null)
                toUpdate.TiktokFollowerCount = influencerDTO.TiktokFollowerCount;

            return toUpdate;
        }
    }
}
