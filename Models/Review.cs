using System.Runtime.InteropServices;

namespace InfluencersPlatformBackend.Models
{
    public class Review
    {
        public  int Id { get; set; }
        public int InfluencerId { get; set; } //of a user, not a profile
        public int CompanyId { get; set; } //of a user, not a profile
        public string? Name { get; set; }
        public string Description { get; set; }
        public int? Stars { get; set; }
        public bool Verified { get; set; }

        public Review() { }
    }
}
