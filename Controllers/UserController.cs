using InfluencersPlatformBackend.Auth;
using InfluencersPlatformBackend.Data;
using InfluencersPlatformBackend.DTOs.UserDTOs;
using InfluencersPlatformBackend.Mappers;
using InfluencersPlatformBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
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
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            var User = await _context.Users.FindAsync(id);
            if (User == null || User.IsDeleted)
                return NotFound();

            return Ok(User.ToUserDTO());
        }

        [HttpGet]
        public async Task<IActionResult> GetUserList()
        {
            var Users = await _context.Users
                .Where(u => !u.IsDeleted).Select(s => s.ToUserDTO()).ToListAsync();

            if (Users == null)
                return NotFound();

            return Ok(Users);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateUser(UserManager<User> userManager, [FromBody] CreateUserRequestDTO userDTO)
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
            //TODO: create method for creating company

            return Created();
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserManager<User> userManager, JwtTokenService jwtTokenService, HttpContext httpContext, [FromBody] LoginUserRequestDTO userDTO)
        {
            //check user exists
            var user = await userManager.FindByNameAsync(userDTO.UserName);
            if (user == null)
            {
                return UnprocessableEntity("User does not exist.");
            }

            var isPasswordValid = await userManager.CheckPasswordAsync(user, userDTO.Password);
            if (!isPasswordValid)
                //TODO: 401 Unauthorized
                return UnprocessableEntity("Username or password is incorrect.");

            var roles = await userManager.GetRolesAsync(user);

            var expiresAt = DateTime.UtcNow.AddDays(3);
            var accessToken = jwtTokenService.CreateAccessToken(user.UserName, user.Id, roles);
            var refreshToken = jwtTokenService.CreateRefreshToken(user.Id, expiresAt);

            var cookieOptions = new CookieOptions 
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Lax,
                Expires = expiresAt
                //Secure = true //TODO: when site has ssl, enable this so that it is https-only
            };

            httpContext.Response.Cookies.Append("RefreshToken", refreshToken, cookieOptions);

            return Ok(new SuccessfulLoginDTO(accessToken));
        }

//TODO: Do i need these methods?
/*

[HttpPut("{id}")]
public async Task<IActionResult> UpdateWholeUser([FromRoute] string id, [FromBody] PutUserRequestDTO UserDTO)
{

    var User = _context.Users.FirstOrDefault(c => c.Id == id);

    if (User == null) return NotFound();

    User = UserDTO.FromPutUserRequestToUser(User);
    await _context.SaveChangesAsync();
    return Ok(User.ToUserDTO());
}

[HttpPatch("{id}")]
public async Task<IActionResult> UpdateUserPartially([FromRoute] string id, [FromBody] PatchUserRequestDTO patchUserDTO)
{
    // Retrieve the User from the database
    var User = await _context.Users.FirstOrDefaultAsync(c => c.Id == id);

    // If the User is not found, return 404 Not Found
    if (User == null) return NotFound();

    // Only update the fields that are not null in the patch request
    User = patchUserDTO.FromPatchUserRequestToUser(User);

    // Save changes to the database
    await _context.SaveChangesAsync();

    // Return the updated User
    return Ok(User.ToUserDTO());
}*/

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            var User = _context.Users.FirstOrDefault(c => c.Id == id);

            if (User == null || User.IsDeleted) 
                return NotFound();

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
