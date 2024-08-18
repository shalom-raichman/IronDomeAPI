using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IronDomeAPI.Services;
using IronDomeAPI.Models;

namespace IronDomeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttacksController : ControllerBase
    {
        [HttpPost]
        [Produces("Application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult CreateAttack([FromBody] Attack newAttack)
        {
            Guid newAttackId = Guid.NewGuid();
            newAttack.id = newAttackId;
            DbService.AttacksList.Add(newAttack);
            return StatusCode(StatusCodes.Status201Created, new { succes = true, attack = newAttack });
        }
        
    }
}
