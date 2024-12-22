namespace InfluencersPlatformBackend.DTOs.UserDTOs
{
    public class SuccessfulLoginDTO
    {
        public SuccessfulLoginDTO(string accessToken)
        {
            AccessToken = accessToken;
        }

        public string AccessToken { get; set; }
    }
}
