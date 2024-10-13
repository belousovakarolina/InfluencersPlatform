using InfluencersPlatformBackend.DTOs.UserDTOs;
using InfluencersPlatformBackend.Models;
using System.Runtime.CompilerServices;

namespace InfluencersPlatformBackend.Mappers
{
    public static class UserMapper
    {
        public static GetUserRequestDTO ToUserDTO (this User user)
        {
            return new GetUserRequestDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                InfluencerProfileId = user.InfluencerProfileId.HasValue ? (int)user.InfluencerProfileId : (int?)null,
                CompanyProfileId = user.CompanyProfileId.HasValue ? (int)user.CompanyProfileId : (int?)null,
                Role = user.Role
            };
        }

        public static User FromCreateUserRequestToUser (this CreateUserRequestDTO userDTO)
        {
            return new User
            {
                Name = userDTO.Name,
                Email = userDTO.Email,
                Password = userDTO.Password,
                Phone = userDTO.Phone,
                Role = userDTO.Role
            };
        }

        public static User FromPutUserRequestToUser (this PutUserRequestDTO userDTO, User toUpdate)
        {
            toUpdate.Name = userDTO.Name;
            toUpdate.Email = userDTO.Email;
            toUpdate.Password = userDTO.Password;
            toUpdate.Phone = userDTO.Phone;
            toUpdate.IsDeleted = userDTO.IsDeleted;
            if (userDTO.InfluencerProfileId != null)
                toUpdate.InfluencerProfileId = userDTO.InfluencerProfileId;
            if (userDTO.CompanyProfileId != null)
                toUpdate.CompanyProfileId = userDTO.CompanyProfileId;
            toUpdate.Role = userDTO.Role;

            return toUpdate;
        }
    }
}
