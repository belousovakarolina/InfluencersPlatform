using InfluencersPlatformBackend.Models;
using System.ComponentModel.DataAnnotations;

namespace InfluencersPlatformBackend.DTOs.CompanyProfileDTOs
{
    public class PutCompanyProfileRequestDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string? Description { get; set; }
        public double? YearlyIncome { get; set; }
        public PutCompanyProfileRequestDTO() { }
    }
}
