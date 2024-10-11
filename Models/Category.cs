namespace InfluencersPlatformBackend.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FollowersCountFrom { get; set; }
        public int FollowersCountTo { get; set; }
        public List<InfluencerProfile> Influencers { get; set; }
        public Category() { }
    }
}
