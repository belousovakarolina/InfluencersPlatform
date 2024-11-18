using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace InfluencersPlatformBackend.Models
{
    public class Review
    {
        public  int Id { get; set; }
        public string InfluencerId { get; set; } //of a user, not a profile
        public User? Influencer { get; set; }
        public string CompanyId { get; set; } //of a user, not a profile
        public User? Company { get; set; }

        [Required]
        public required string UserId { get; set; } //creator of the review
        public User User { get; set; } //creator of the review
        public string? Name { get; set; }
        public string Description { get; set; }
        public int? Stars { get; set; }
        public bool Verified { get; set; }
        public DateTime CreatedDate {  get; set; } =  DateTime.Now; 

        public Review() { }
    }
}
