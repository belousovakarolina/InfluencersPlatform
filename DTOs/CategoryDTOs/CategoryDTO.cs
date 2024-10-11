using InfluencersPlatformBackend.Models;

namespace InfluencersPlatformBackend.DTOs.CategoryDTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FollowersCountFrom { get; set; }
        public int FollowersCountTo { get; set; }
        public List<InfluencerProfile> Influencers { get; set; }
        public CategoryDTO() { }
    }
}
