using InfluencersPlatformBackend.Models;

namespace InfluencersPlatformBackend.DTOs.CompanyProfileDTOs
{
    public class PatchCompanyProfileRequestDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? YearlyIncome { get; set; }
    }
}
