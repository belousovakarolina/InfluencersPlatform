using Microsoft.AspNetCore.Identity;

namespace InfluencersPlatformBackend.Models
{
    public class User: IdentityUser
    {
        //by default in IdentityUser
        //public int Id { get; set; }
        //public string Name { get; set; }
        //public string Email { get; set; }
        //public string Password { get; set; }
        //public string Phone { get; set; }
        public bool IsDeleted { get; set; }

        // One-to-one relationship with InfluencerProfile (nullable)
        public int? InfluencerProfileId { get; set; }
        public virtual InfluencerProfile InfluencerProfile { get; set; }

        // One-to-one relationship with CompanyProfile (nullable)
        public int? CompanyProfileId { get; set; }
        public virtual CompanyProfile CompanyProfile { get; set; }
        public string Role { get; set; } //Administrator, Influencer, Company

        public User() { }
    }
}
