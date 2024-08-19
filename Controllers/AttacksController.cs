using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IronDomeAPI.Services;
using IronDomeAPI.Models;
using Microsoft.EntityFrameworkCore;
using IronDomeAPI.MiddleWares.Global;
using IronDomeAPI.MiddlEWares.Attack;

namespace IronDomeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttacksController : ControllerBase
    {
        //private DbContext _context;

        //public AttacksController(DbContext context)
        //{
        //    _context = context;
        //}

        // GET: get all atacks 
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
        
        // GET: get status by id
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

        // POST: create new attack
        [HttpPost]
        [Produces("Application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult CreateAttack([FromBody] Attack newAttack)
        {
            Guid newAttackId = Guid.NewGuid();
            newAttack.id = newAttackId;
            newAttack.status = "Pending";
            DbService.AttacksList.Add(newAttack);
            //_context.Add(newAttack);
            //_context.SaveChanges();
            return StatusCode(
                StatusCodes.Status201Created,
                new { succes = true, attack = newAttack }
            );
        }

        // POST: Define attack missiles amount
        //[HttpPost]
        //[Produces("Application/json")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //public IActionResult DefineAttackMissiles([FromBody] Attack attack)
        //{
            
        //    return StatusCode(StatusCodes.Status201Created, new { succes = true, attack = "newAttack" });
        //}
        
        // POST: start attack
        [HttpPost("{id}/start")]
        [Produces("Application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult StartAttack(Guid id)
        {
            
            Attack attack = DbService.AttacksList.FirstOrDefault(attack => attack.id == id);
            if (attack == null) return StatusCode(404, new {success = false, nessage = "attack not found"});
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
            attack.StartedAt = DateTime.Now;
            return StatusCode(
                StatusCodes.Status200OK,
                 new { message = "attack started.", TaskId =  attackTask.Id});
        }

        // POST: intercept missile
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
                 new {
                        message = "Attack intercepted.",
                        status = "Success"
                 });
        }
        
    }
}
