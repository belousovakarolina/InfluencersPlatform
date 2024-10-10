using InfluencersPlatformBackend.Data;
using Microsoft.AspNetCore.Mvc;

namespace InfluencersPlatformBackend.Controllers
{
    [Route("api/v1/company")]
    [ApiController]
    public class CompanyProfileController: ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public CompanyProfileController(ApplicationDBContext context)
        {
            _context = context;
        }
    }
}
