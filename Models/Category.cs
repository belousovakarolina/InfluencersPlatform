namespace InfluencersPlatformBackend.Models
{
    //ypac dideli - 1 000 001
    //dideli - 100 001 - 1 000 000
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
