using InfluencersPlatformBackend.Data;
using Microsoft.AspNetCore.Mvc;

namespace InfluencersPlatformBackend.Controllers
{
    [Route("api/v1/review")]
    [ApiController]
    public class ReviewController: ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public ReviewController(ApplicationDBContext context)
        {
            _context = context;
        }
    }
}
