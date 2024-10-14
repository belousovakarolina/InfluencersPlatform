using InfluencersPlatformBackend.Models;

namespace InfluencersPlatformBackend.DTOs.CategoryDTOs
{
    public class GetCategoryRequestDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FollowersCountFrom { get; set; }
        public int FollowersCountTo { get; set; }
        public GetCategoryRequestDTO() { }
    }
}
