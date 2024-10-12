using InfluencersPlatformBackend.Data;
using InfluencersPlatformBackend.DTOs.UserDTOs;
using InfluencersPlatformBackend.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var User = newUserRequest.FromCreateUserRequestToUser();
            _context.Users.Add(User);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { id = User.Id }, User.ToUserDTO());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWholeUser([FromRoute] int id, [FromBody] PutUserRequestDTO UserDTO)
        {
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

            if (User == null) return NotFound();

            _context.Users.Remove(User);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
