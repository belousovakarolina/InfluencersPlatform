namespace InfluencersPlatformBackend.DTOs.UserDTOs
{
    public class GetUserRequestDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? InfluencerProfileId { get; set; }
        public int? CompanyProfileId { get; set; }
        public string Role { get; set; }
        public GetUserRequestDTO() { }
    }
}
