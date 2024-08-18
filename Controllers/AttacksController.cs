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
        [HttpGet]
        public IActionResult GetAttacks()
        {
            return StatusCode(
            StatusCodes.Status200OK,
            new
            {
                success = true,
                attacks = DbService.AttacksList.ToArray()
            }
            );
        }
        
        [HttpGet("{id}/status")]
        public IActionResult AtackStatus(Guid id)
        {
            Attack attack = DbService.AttacksList.FirstOrDefault(attack => attack.id == id);
            if (attack == null) return NotFound();
            return StatusCode(
            StatusCodes.Status200OK,
            new
            {
                id = attack.id,
                status = attack.status,
                startAt = new DateTime()
            }
            );
        }


        [HttpPost]
        [Produces("Application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult CreateAttack([FromBody] Attack newAttack)
        {
            Guid newAttackId = Guid.NewGuid();
            newAttack.id = newAttackId;
            newAttack.status = "Pending";
            DbService.AttacksList.Add(newAttack);
            return StatusCode(StatusCodes.Status201Created, new { succes = true, attack = newAttack });
        }
        
        [HttpPost("{id}/start")]
        [Produces("Application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult StartAttack(Guid id)
        {
            
            Attack attack = DbService.AttacksList.FirstOrDefault(attack => attack.id == id);
            if (attack == null) return NotFound();
            if(attack.status == "Completed")
            {
                return StatusCode(
                    400,
                    new
                    {
                        error = "Cannot start an attack that has already been completed."
                    });
            }
            Task attackTask = Task.Run(() => 
            {
                Task.Delay(10000);
            });

            attack.status = "in progres";
            return StatusCode(
                StatusCodes.Status200OK,
                 new { message = "attack started.", TaskId =  attackTask.Id});
        }

        [HttpPost("{id}/intercept")]
        [Produces("Application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult InterceptMissile(Guid id)
        {
            
            Attack attack = DbService.AttacksList.FirstOrDefault(attack => attack.id == id);
            if (attack == null) return NotFound();
            if(attack.status == "Completed")
            {
                return StatusCode(
                    400,
                    new
                    {
                        error = "Cannot intercept an attack that has already been completed."
                    });
            }
            Task attackTask = Task.Run(() => 
            {
                Task.Delay(10000);
            });

            attack.status = "Completed";
            return StatusCode(
                StatusCodes.Status200OK,
                 new { message = "attack started.", TaskId =  attackTask.Id});
        }
        
    }
}
