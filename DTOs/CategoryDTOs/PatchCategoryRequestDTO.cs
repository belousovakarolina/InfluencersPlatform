using System.ComponentModel.DataAnnotations;

namespace InfluencersPlatformBackend.DTOs.CategoryDTOs
{
    public class PatchCategoryRequestDTO
    {
        public string? Name { get; set; } // Nullable string for partial updates

        [Range(1, int.MaxValue, ErrorMessage = "FollowersCountFrom must be present and greater than zero")]
        public int? FollowersCountFrom { get; set; } // Nullable int for partial updates

        [Range(1, int.MaxValue, ErrorMessage = "FollowersCountTo must be present and greater than zero")]
        public int? FollowersCountTo { get; set; } // Nullable int for partial updates
    }
}
