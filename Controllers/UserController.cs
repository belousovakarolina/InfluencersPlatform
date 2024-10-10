using InfluencersPlatformBackend.Data;
using Microsoft.AspNetCore.Mvc;

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
    }
}
