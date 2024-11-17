using InfluencersPlatformBackend.Auth;

namespace InfluencersPlatformBackend.DTOs.UserDTOs
{
    public class GetUserRequestDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? InfluencerProfileId { get; set; } //profile, not a user
        public int? CompanyProfileId { get; set; } //profile, not a user
        public string Roles { get; set; }
        public GetUserRequestDTO() { }
    }
}
