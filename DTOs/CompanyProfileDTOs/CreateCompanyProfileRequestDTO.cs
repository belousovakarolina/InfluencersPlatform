using InfluencersPlatformBackend.Models;

namespace InfluencersPlatformBackend.DTOs.CompanyProfileDTOs
{
    public class CreateCompanyProfileRequestDTO
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double? YearlyIncome { get; set; }
        public CreateCompanyProfileRequestDTO() { }
    }
}
