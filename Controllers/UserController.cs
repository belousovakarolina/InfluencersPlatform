using InfluencersPlatformBackend.Auth;
using InfluencersPlatformBackend.Data;
using InfluencersPlatformBackend.DTOs.UserDTOs;
using InfluencersPlatformBackend.Mappers;
using InfluencersPlatformBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Numerics;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace InfluencersPlatformBackend.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public UserController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> GetUser(UserManager<User> userManager, [FromRoute] int id)
        {
            var User = await _context.Users.FindAsync(id);
            if (User == null || User.IsDeleted)
                return NotFound();

            return Ok(User.ToUserDTO(userManager));
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> GetUserList(UserManager<User> userManager)
        {
            // Retrieve all users first
            var users = await _context.Users
                .Where(u => !u.IsDeleted)
                .ToListAsync();

            // Map users to DTOs, fetching roles one at a time
            var userDTOs = new List<GetUserRequestDTO>();
            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);
                userDTOs.Add(new GetUserRequestDTO
                {
                    Id = user.Id,
                    Email = user.Email,
                    InfluencerProfileId = user.InfluencerProfileId.HasValue ? (int)user.InfluencerProfileId : (int?)null,
                    CompanyProfileId = user.CompanyProfileId.HasValue ? (int)user.CompanyProfileId : (int?)null,
                    Roles = string.Join(", ", roles)
                });
            }

            return Ok(userDTOs);
        }


        [HttpPost]
        [Route("influencer")]
        public async Task<IActionResult> CreateInfluencerUser(UserManager<User> userManager, [FromBody] CreateUserRequestDTO userDTO)
        {
            //TOD: duomenu patikra
            /*if (!newUserRequest.Phone.StartsWith("3706") |
                newUserRequest.Phone.Length != 11)
                return UnprocessableEntity(new
                {
                    message = "Phone must start with 370 and have 11 characters in total."
                });

            if (!Regex.IsMatch(newUserRequest.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return UnprocessableEntity(new
                {
                    message = "Email must not have blank spaces, must have '@' symbol and a domain ending."
                });*/

            //check user exists
            var user = await userManager.FindByNameAsync(userDTO.UserName);
            if (user != null)
            {
                return UnprocessableEntity("Username already taken.");
            }

            var newUser = new User()
            {
                Email = userDTO.Email,
                UserName = userDTO.UserName
            };

            //TODO: wrap creation and role addition in one transaction
            var createUserResult = await userManager.CreateAsync(newUser, userDTO.Password);
            if (!createUserResult.Succeeded)
                return UnprocessableEntity();

            await userManager.AddToRoleAsync(newUser, UserRoles.Influencer);

            return Created();
        }
        [HttpPost]
        [Route("company")]
        public async Task<IActionResult> CreateCompanyUser(UserManager<User> userManager, [FromBody] CreateUserRequestDTO userDTO)
        {
            //TOD: duomenu patikra
            /*if (!newUserRequest.Phone.StartsWith("3706") |
                newUserRequest.Phone.Length != 11)
                return UnprocessableEntity(new
                {
                    message = "Phone must start with 370 and have 11 characters in total."
                });

            if (!Regex.IsMatch(newUserRequest.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return UnprocessableEntity(new
                {
                    message = "Email must not have blank spaces, must have '@' symbol and a domain ending."
                });*/

            //check user exists
            var user = await userManager.FindByNameAsync(userDTO.UserName);
            if (user != null)
            {
                return UnprocessableEntity("Username already taken.");
            }

            var newUser = new User()
            {
                Email = userDTO.Email,
                UserName = userDTO.UserName
            };

            //TODO: wrap creation and role addition in one transaction
            var createUserResult = await userManager.CreateAsync(newUser, userDTO.Password);
            if (!createUserResult.Succeeded)
                return UnprocessableEntity();

            await userManager.AddToRoleAsync(newUser, UserRoles.Company);

            return Created();
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserManager<User> userManager, JwtTokenService jwtTokenService, SessionService sessionService, [FromBody] LoginUserRequestDTO userDTO)
        {
            //check user exists
            var user = await userManager.FindByNameAsync(userDTO.UserName);
            if (user == null)
            {
                return UnprocessableEntity("User does not exist.");
            }

            var isPasswordValid = await userManager.CheckPasswordAsync(user, userDTO.Password);
            if (!isPasswordValid)
                return Unauthorized("Username or password is incorrect.");

            var roles = await userManager.GetRolesAsync(user);

            var sessionId = Guid.NewGuid();
            var expiresAt = DateTime.UtcNow.AddDays(3);
            var accessToken = jwtTokenService.CreateAccessToken(user.UserName, user.Id, roles);
            var refreshToken = jwtTokenService.CreateRefreshToken(sessionId, user.Id, expiresAt);

            await sessionService.CreateSessionAsync(sessionId, user.Id, refreshToken, expiresAt);

            var cookieOptions = new CookieOptions 
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Lax,
                Expires = expiresAt
                //Secure = true //TODO: when site has ssl, enable this so that it is https-only
            };

            this.HttpContext.Response.Cookies.Append("RefreshToken", refreshToken, cookieOptions);

            return Ok(new SuccessfulLoginDTO(accessToken));
        }

        [HttpPost]
        [Route("accessToken")]
        public async Task<IActionResult> AccessToken(UserManager<User> userManager, JwtTokenService jwtTokenService, SessionService sessionService)
        {
            if (!this.HttpContext.Request.Cookies.TryGetValue("RefreshToken", out var refreshToken))
            {
                return UnprocessableEntity("There is no refresh token in cookies.");
            }
            if (!jwtTokenService.TryParseRefreshToken(refreshToken, out var claims))
            {
                return UnprocessableEntity("Refresh token is not valid.");
            }

            var sessionId = claims.FindFirstValue("SessionId");
            if (string.IsNullOrWhiteSpace(sessionId))
            {
                return UnprocessableEntity("Session does not exist.");
            }

            var sessionIdAsGuid = Guid.Parse(sessionId);
            if (!await sessionService.IsSessionValidAsync(sessionIdAsGuid, refreshToken))
            {
                return UnprocessableEntity("Session is not valid.");
            }

            var userId = claims.FindFirstValue(JwtRegisteredClaimNames.Sub);
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return UnprocessableEntity("User does not exist.");
            }

            var roles = await userManager.GetRolesAsync(user);

            var expiresAt = DateTime.UtcNow.AddDays(3);
            var accessToken = jwtTokenService.CreateAccessToken(user.UserName, user.Id, roles);
            var newRefreshToken = jwtTokenService.CreateRefreshToken(sessionIdAsGuid, user.Id, expiresAt);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Lax,
                Expires = expiresAt
                //Secure = true //TODO: when site has ssl, enable this so that it is https-only
            };

            this.HttpContext.Response.Cookies.Append("RefreshToken", newRefreshToken, cookieOptions);

            await sessionService.ExtendSessionAsync(sessionIdAsGuid, newRefreshToken, expiresAt);

            return Ok(new SuccessfulLoginDTO(accessToken));

        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout(UserManager<User> userManager, JwtTokenService jwtTokenService, SessionService sessionService)
        {
            if (!this.HttpContext.Request.Cookies.TryGetValue("RefreshToken", out var refreshToken))
            {
                return UnprocessableEntity("There is no refresh token in cookies.");
            }
            if (!jwtTokenService.TryParseRefreshToken(refreshToken, out var claims))
            {
                return UnprocessableEntity("Refresh token is not valid.");
            }

            var sessionId = claims.FindFirstValue("SessionId");
            if (string.IsNullOrWhiteSpace(sessionId))
            {
                return UnprocessableEntity("Session does not exist.");
            }

            await sessionService.InvalidateSessionAsync(Guid.Parse(sessionId));
            this.HttpContext.Response.Cookies.Delete("RefreshToken");

            return Ok();

        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateWholeUser(UserManager<User> userManager, [FromRoute] string id, [FromBody] PutUserRequestDTO UserDTO)
        {

            var User = _context.Users.FirstOrDefault(c => c.Id == id);

            if (User == null) return NotFound();

            string userId = this.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (!this.HttpContext.User.IsInRole(UserRoles.Admin) && this.HttpContext.User.FindFirstValue(userId) != User.Id)
            {
                return Forbid("You cannot edit this resource.");
            }

            User = UserDTO.FromPutUserRequestToUser(User);
            await _context.SaveChangesAsync();
            return Ok(User.ToUserDTO(userManager));
        }

        [HttpPatch("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUserPartially(UserManager<User> userManager, [FromRoute] string id, [FromBody] PatchUserRequestDTO patchUserDTO)
        {
            // Retrieve the User from the database
            var User = await _context.Users.FirstOrDefaultAsync(c => c.Id == id);

            // If the User is not found, return 404 Not Found
            if (User == null) return NotFound();

            string userId = this.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (!this.HttpContext.User.IsInRole(UserRoles.Admin) && this.HttpContext.User.FindFirstValue(userId) != User.Id)
            {
                return Forbid("You cannot edit this resource.");
            }

            // Only update the fields that are not null in the patch request
            User = patchUserDTO.FromPatchUserRequestToUser(User);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return the updated User
            return Ok(User.ToUserDTO(userManager));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            var User = _context.Users.FirstOrDefault(c => c.Id == id);

            if (User == null || User.IsDeleted) 
                return NotFound();

            string userId = this.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (!this.HttpContext.User.IsInRole(UserRoles.Admin) && this.HttpContext.User.FindFirstValue(userId) != User.Id)
            {
                return Forbid("You cannot  this resource.");
            }

            // Mark the user as deleted
            User.IsDeleted = true;

            //TODO: taip pat i guess istrinti ir jo profili? 
            //kai trinu profili, tai profili istrinu, o useri pazymiu kaip is deleted


            // Save the changes to the database
            _context.Users.Update(User);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
