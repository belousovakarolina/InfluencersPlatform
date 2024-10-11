using System.Runtime.InteropServices;

namespace InfluencersPlatformBackend.Models
{
    public class Review
    {
        public  int Id { get; set; }
        public int InfluencerId { get; set; } //of a user, not a profile
        public User? Influencer { get; set; }
        public int CompanyId { get; set; } //of a user, not a profile
        public User? Company { get; set; }
        public string? Name { get; set; }
        public string Description { get; set; }
        public int? Stars { get; set; }
        public bool Verified { get; set; }
        public bool IsAboutInfluencer { get; set; } //is this review from a company about 
        //a influencer or vice versa?
        public DateTime CreatedDate {  get; set; } =  DateTime.Now; 

        public Review() { }
    }
}
