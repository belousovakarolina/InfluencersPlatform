using InfluencersPlatformBackend.Models;

namespace InfluencersPlatformBackend.DTOs.CompanyProfileDTOs
{
    public class PutCompanyProfileRequestDTO
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double? YearlyIncome { get; set; }
        public PutCompanyProfileRequestDTO() { }
    }
}
