using InfluencersPlatformBackend.Data;
using Microsoft.AspNetCore.Mvc;

namespace InfluencersPlatformBackend.Controllers
{
    [Route("api/v1/influencer")]
    [ApiController]
    public class InfluencerProfileController: ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public InfluencerProfileController(ApplicationDBContext context)
        {
            _context = context;
        }
    }
}
