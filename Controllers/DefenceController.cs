using IronDomeAPI.Models;
using IronDomeAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IronDomeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefenceController : ControllerBase
    {
        //public readonly DbContext _context;
        //DefenceController(DbContext context) 
        //{
        //    _context = context;
        //}


        [HttpPut]
        public IActionResult Missiles(Defence defence)
        {
            DefenceService service = new DefenceService();

            return Ok(service);
        }
    }
}
