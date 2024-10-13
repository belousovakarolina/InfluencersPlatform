using InfluencersPlatformBackend.Data;
using InfluencersPlatformBackend.DTOs.UserDTOs;
using InfluencersPlatformBackend.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Text.RegularExpressions;

namespace InfluencersPlatformBackend.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    public class UserController: ControllerBase
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
            if (User == null)
                return NotFound();

            return Ok(User.ToUserDTO());
        }

        [HttpGet]
        public async Task<IActionResult> GetUserList()
        {
            var Users = await _context.Users
                .Select(s => s.ToUserDTO()).ToListAsync();

            if (Users == null)
                return NotFound();

            return Ok(Users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestDTO newUserRequest)
        {
            if (newUserRequest.Role != "Administrator" && 
                newUserRequest.Role != "Influencer" &&
                newUserRequest.Role != "Company")
                return UnprocessableEntity(new
                {
                    message = "User must be one of these roles: Administrator, Influencer, Company."
                });

            if (!newUserRequest.Phone.StartsWith("3706") |
                newUserRequest.Phone.Length != 11)
                return UnprocessableEntity(new
                {
                    message = "Phone must start with 370 and have 11 characters in total."
                });

            if (!Regex.IsMatch(newUserRequest.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return UnprocessableEntity(new
                {
                    message = "Email must not have blank spaces, must have '@' symbol and a domain ending."
                });

            var User = newUserRequest.FromCreateUserRequestToUser();
            _context.Users.Add(User);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { id = User.Id }, User.ToUserDTO());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWholeUser([FromRoute] int id, [FromBody] PutUserRequestDTO UserDTO)
        {
            if (UserDTO.Role != "Administrator" &&
                UserDTO.Role != "Influencer" &&
                UserDTO.Role != "Company")
                return UnprocessableEntity(new
                {
                    message = "User must be one of these roles: Administrator, Influencer, Company."
                });

            if (UserDTO.CompanyProfileId  != null && UserDTO.Role != "Company")
                return UnprocessableEntity(new
                {
                    message = "Can only add a Company Profile for a user with a Company role."
                });

            if (UserDTO.InfluencerProfileId != null && UserDTO.Role != "Influencer")
                return UnprocessableEntity(new
                {
                    message = "Can only add an Influencer Profile for a user with an Influencer role."
                });

            var User = _context.Users.FirstOrDefault(c => c.Id == id);

            if (User == null) return NotFound();

            User = UserDTO.FromPutUserRequestToUser(User);
            await _context.SaveChangesAsync();
            return Ok(User.ToUserDTO());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            var User = _context.Users.FirstOrDefault(c => c.Id == id);

            if (User == null || User.IsDeleted) 
                return NotFound();

            // Mark the user as deleted
            User.IsDeleted = true;

            // Save the changes to the database
            _context.Users.Update(User);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
