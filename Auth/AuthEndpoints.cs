using InfluencersPlatformBackend.DTOs.UserDTOs;
using InfluencersPlatformBackend.Models;
using Microsoft.AspNetCore.Identity;

namespace InfluencersPlatformBackend.Auth
{
    public static class AuthEndpoints
    {
        public static void AddAuthApi(this WebApplication app)
        {
            //register
            app.MapPost("api/v1/users", async (UserManager<User> userManager, CreateUserRequestDTO userDTO) =>
            {
                //check user exists
                var user = await userManager.FindByNameAsync(userDTO.UserName);
                if (user == null)
                {
                    return Results.UnprocessableEntity("Username already taken.");
                }

                var newUser = new User()
                {
                    Email = userDTO.Email,
                    UserName = userDTO.UserName
                };

                //TODO: wrap creation and role addition in one transaction
                var createUserResult = await userManager.CreateAsync(newUser, userDTO.Password);
                if (!createUserResult.Succeeded)
                    return Results.UnprocessableEntity();

                await userManager.AddToRoleAsync(newUser, UserRoles.Influencer);
                //TODO: create method for creating company

                return Results.Created();
            });
        }
    }
}
