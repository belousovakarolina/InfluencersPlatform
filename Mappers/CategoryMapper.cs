﻿using InfluencersPlatformBackend.DTOs.CategoryDTOs;
using InfluencersPlatformBackend.Models;
using System.Runtime.CompilerServices;

namespace InfluencersPlatformBackend.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryDTO ToCategoryDTO(this Category category)
        {
            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                FollowersCountFrom = category.FollowersCountFrom,
                FollowersCountTo = category.FollowersCountTo,
                Influencers = category.Influencers
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
    }
}
