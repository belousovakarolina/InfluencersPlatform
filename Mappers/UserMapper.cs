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
                Email = user.Email,
                InfluencerProfileId = user.InfluencerProfileId.HasValue ? (int)user.InfluencerProfileId : (int?)null,
                CompanyProfileId = user.CompanyProfileId.HasValue ? (int)user.CompanyProfileId : (int?)null
            };
        }

        public static User FromCreateUserRequestToUser (this CreateUserRequestDTO userDTO)
        {
            return new User
            {
                Email = userDTO.Email
            };
        }

        public static User FromPutUserRequestToUser (this PutUserRequestDTO userDTO, User toUpdate)
        {
            toUpdate.Email = userDTO.Email;
            toUpdate.IsDeleted = userDTO.IsDeleted;

            return toUpdate;
        }

        public static User FromPatchUserRequestToUser (this PatchUserRequestDTO userDTO, User toUpdate)
        {
            if (userDTO.Email != null) 
                toUpdate.Email = userDTO.Email;
            if (userDTO.IsDeleted != null) 
                toUpdate.IsDeleted = (bool)userDTO.IsDeleted;
            return toUpdate;
        }
    }
}
//TODO: all users dtos: delete unnecessary variables
//TODO: add roles
