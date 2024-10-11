using InfluencersPlatformBackend.Models;

namespace InfluencersPlatformBackend.DTOs.CategoryDTOs
{
    public class CreateCategoryRequestDTO
    {
        public string Name { get; set; }
        public int FollowersCountFrom { get; set; }
        public int FollowersCountTo { get; set; }
        public CreateCategoryRequestDTO() { }
    }
}
