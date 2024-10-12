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
                Roles = user.Roles
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
                Roles = userDTO.Roles
            };
        }

        public static User FromPutUserRequestToUser (this PutUserRequestDTO userDTO, User toUpdate)
        {
            toUpdate.Name = userDTO.Name;
            toUpdate.Email = userDTO.Email;
            toUpdate.Password = userDTO.Password;
            toUpdate.Phone = userDTO.Phone;
            toUpdate.IsDeleted = userDTO.IsDeleted;
            toUpdate.Roles = userDTO.Roles;

            return toUpdate;
        }
    }
}
