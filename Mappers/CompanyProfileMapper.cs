using InfluencersPlatformBackend.DTOs.CompanyProfileDTOs;
using InfluencersPlatformBackend.Models;

namespace InfluencersPlatformBackend.Mappers
{
    public static class CompanyProfileMapper
    {
        public static CompanyProfileDTO ToCompanyProfileDTO(this CompanyProfile profile)
        {
            return new CompanyProfileDTO
            {
                Id = profile.Id,
                UserId = profile.UserId,
                User = profile.User,
                Name = profile.Name,
                Description = profile.Description,
                YearlyIncome = profile.YearlyIncome,
                Reviews = profile.Reviews
            };
        }
    }
}
