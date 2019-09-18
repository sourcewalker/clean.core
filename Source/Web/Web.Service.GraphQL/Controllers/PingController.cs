using Microsoft.AspNetCore.Mvc;

namespace Web.Service.GraphQL.Controllers
{
    [ApiController]
    [RequireHttps]
    public class PingController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return Redirect($"{Request.Scheme}://{Request.Host.ToUriComponent()}/ui/playground");
        }
    }
}
