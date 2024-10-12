namespace InfluencersPlatformBackend.DTOs.CategoryDTOs
{
    public class PutCategoryRequestDTO
    {
        public string Name { get; set; }
        public int FollowersCountFrom { get; set; }
        public int FollowersCountTo { get; set; }
        public PutCategoryRequestDTO() { }
    }
}
