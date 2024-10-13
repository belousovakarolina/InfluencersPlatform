using System.ComponentModel.DataAnnotations;

namespace InfluencersPlatformBackend.DTOs.CategoryDTOs
{
    public class PutCategoryRequestDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "FollowersCountFrom is required")]
        [Range(1, int.MaxValue, ErrorMessage = "FollowersCountFrom must be present and greater than zero")]
        public int FollowersCountFrom { get; set; }

        [Required(ErrorMessage = "FollowersCountTo is required")]
        [Range(1, int.MaxValue, ErrorMessage = "FollowersCountTo must be present and greater than zero")]
        public int FollowersCountTo { get; set; }
        public PutCategoryRequestDTO() { }
    }
}
