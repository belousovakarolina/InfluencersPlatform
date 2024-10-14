using InfluencersPlatformBackend.DTOs.CategoryDTOs;
using InfluencersPlatformBackend.Models;
using System.Runtime.CompilerServices;

namespace InfluencersPlatformBackend.Mappers
{
    public static class CategoryMapper
    {
        public static GetCategoryRequestDTO ToCategoryDTO(this Category category)
        {
            return new GetCategoryRequestDTO
            {
                Id = category.Id,
                Name = category.Name,
                FollowersCountFrom = category.FollowersCountFrom,
                FollowersCountTo = category.FollowersCountTo
            };
        }

        public static Category FromCreateCategoryRequestToCategory (this CreateCategoryRequestDTO createCategoryRequest)
        {
            return new Category
            {
                Name = createCategoryRequest.Name,
                FollowersCountFrom = createCategoryRequest.FollowersCountFrom,
                FollowersCountTo = createCategoryRequest.FollowersCountTo
            };
        }

        public static Category FromPutCategoryRequestToCategory(this PutCategoryRequestDTO putCategoryRequest, Category toUpdate)
        {
            toUpdate.Name = putCategoryRequest.Name;
            toUpdate.FollowersCountFrom = putCategoryRequest.FollowersCountFrom;
            toUpdate.FollowersCountTo = putCategoryRequest.FollowersCountTo;

            return toUpdate;
        }

        public static Category FromPatchCategoryRequestToCategory (this PatchCategoryRequestDTO patchCategoryRequest, Category toUpdate)
        {
            if (patchCategoryRequest.Name != null)
                toUpdate.Name = patchCategoryRequest.Name;

            if (patchCategoryRequest.FollowersCountFrom.HasValue)
                toUpdate.FollowersCountFrom = (int)patchCategoryRequest.FollowersCountFrom;

            if (patchCategoryRequest.FollowersCountTo.HasValue)
                toUpdate.FollowersCountTo = (int)patchCategoryRequest.FollowersCountTo;

            return toUpdate;
        }
    }
}
