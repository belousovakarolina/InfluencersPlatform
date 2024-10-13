using InfluencersPlatformBackend.DTOs.CategoryDTOs;
using InfluencersPlatformBackend.DTOs.CompanyProfileDTOs;
using InfluencersPlatformBackend.Models;

namespace InfluencersPlatformBackend.Mappers
{
    public static class CompanyProfileMapper
    {
        public static GetCompanyProfileRequestDTO ToCompanyProfileDTO(this CompanyProfile profile)
        {
            return new GetCompanyProfileRequestDTO
            {
                Id = profile.Id,
                UserId = profile.UserId,
                Name = profile.Name,
                Description = profile.Description,
                YearlyIncome = profile.YearlyIncome
            };
        }

        public static CompanyProfile FromCreateCompanyProfileRequestToCompanyProfile(this CreateCompanyProfileRequestDTO createCompanyProfileRequest)
        {
            return new CompanyProfile
            {
                UserId = createCompanyProfileRequest.UserId,
                Name = createCompanyProfileRequest.Name,
                Description = createCompanyProfileRequest.Description,
                YearlyIncome = createCompanyProfileRequest.YearlyIncome
            };
        }

        public static CompanyProfile FromPutCompanyProfileRequestToCompanyProfile(this PutCompanyProfileRequestDTO putCompanyProfileRequest, CompanyProfile toUpdate)
        {
            toUpdate.Name = putCompanyProfileRequest.Name;
            toUpdate.Description = putCompanyProfileRequest.Description;
            toUpdate.YearlyIncome = putCompanyProfileRequest.YearlyIncome;

            return toUpdate;
        }

        public static CompanyProfile FromPatchCompanyProfileRequestToCompanyProfile (this PatchCompanyProfileRequestDTO patchCompanyProfileRequest, CompanyProfile toUpdate)
        {
            if (patchCompanyProfileRequest.Name != null)
                toUpdate.Name = patchCompanyProfileRequest.Name;
            if (patchCompanyProfileRequest.Name != null)
                toUpdate.Description = patchCompanyProfileRequest.Description;
            if (patchCompanyProfileRequest.YearlyIncome.HasValue)
                toUpdate.YearlyIncome = patchCompanyProfileRequest.YearlyIncome;

            return toUpdate;
        }
    }
}
