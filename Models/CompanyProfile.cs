using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfluencersPlatformBackend.Models
{
    public class CompanyProfile
    {
        public  int Id { get; set; }
        // One-to-one relationship with User
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double? YearlyIncome { get; set; }
        public List<Review> Reviews { get; set; } = new List<Review>();
        public CompanyProfile() { }
    }
}
