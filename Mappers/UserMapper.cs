using InfluencersPlatformBackend.DTOs;
using InfluencersPlatformBackend.Models;

namespace InfluencersPlatformBackend.Mappers
{
    public static class UserMapper
    {
        public static UserDTO ToUserDTO (this User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                IsDeleted = user.IsDeleted
            };
        }
    }
}
