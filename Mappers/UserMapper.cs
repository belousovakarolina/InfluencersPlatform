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

            return toUpdate;
        }

        public static User FromPatchUserRequestToUser (this PatchUserRequestDTO userDTO, User toUpdate)
        {
            if (userDTO.Name != null)
                toUpdate.Name = userDTO.Name;
            if (userDTO.Email != null) 
                toUpdate.Email = userDTO.Email;
            if (userDTO.Password != null) 
                toUpdate.Password = userDTO.Password;
            if (userDTO.Phone != null) 
                toUpdate.Phone = userDTO.Phone;
            if (userDTO.IsDeleted != null) 
                toUpdate.IsDeleted = (bool)userDTO.IsDeleted;
            return toUpdate;
        }
    }
}
