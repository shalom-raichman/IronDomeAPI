using IronDomeAPI.Models;
using IronDomeAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IronDomeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefenceController : ControllerBase
    {
        [HttpPut]
        public IActionResult Missiles(Defence defence)
        {
            DefenceService service = new DefenceService();

            return Ok(service);
        }
    }
}
