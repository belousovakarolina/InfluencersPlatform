namespace InfluencersPlatformBackend.Auth
{
    public class UserRoles
    {
        public const string Admin = nameof(Admin);
        public const string Influencer = nameof(Influencer);
        public const string Company = nameof(Company);

        public static readonly IReadOnlyCollection<string> All = new[] { Admin, Influencer, Company };
    }
}
