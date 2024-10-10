using InfluencersPlatformBackend.Data;
using Microsoft.AspNetCore.Mvc;

namespace InfluencersPlatformBackend.Controllers
{
    [Route("api/v1/category")]
    [ApiController]
    public class CategoryController: ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public CategoryController(ApplicationDBContext context)
        {
            _context = context;
        }
    }
}
