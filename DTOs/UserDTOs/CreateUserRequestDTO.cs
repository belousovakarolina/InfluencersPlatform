namespace InfluencersPlatformBackend.DTOs.UserDTOs
{
    public class CreateUserRequestDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public List<string> Roles { get; set; }
    }
}
