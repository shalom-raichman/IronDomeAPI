using IronDomeAPI.Data;
using IronDomeAPI.Models;
using IronDomeAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IronDomeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttacksController : ControllerBase
    {
        private IronDomeAPIDbContext _context;

        public AttacksController(IronDomeAPIDbContext context)
        {
            _context = context;
        }

        // GET: get all atacks
        [HttpGet]
        public async Task<IActionResult> GetAttacks()
        {
            var attacks = await _context.Attacks.ToListAsync();
            return StatusCode(
            StatusCodes.Status200OK,
            attacks
            );
        }

        // GET: get status by id
        [HttpGet("{id}/status")]
        public async Task<IActionResult> AtackStatus(Guid id)
        {
            Attack attack = await _context.Attacks.FirstOrDefaultAsync(attack => attack.id == id);
            if (attack == null) return NotFound();
            return StatusCode(
            StatusCodes.Status200OK,
            new
            {
                id = attack.id,
                status = attack.status,
                startAt = attack.StartedAt
            }
            );
        }

        // POST: create new attack
        [HttpPost]
        [Produces("Application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateAttack([FromBody] Attack newAttack)
        {
            Guid newAttackId = Guid.NewGuid();
            newAttack.id = newAttackId;
            newAttack.status = "Pending";
            await _context.AddAsync(newAttack);
            await _context.SaveChangesAsync();
            return StatusCode(
                StatusCodes.Status201Created,
                newAttack
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
        public async Task<IActionResult> StartAttack(Guid id)
        {

            Attack attack = _context.Attacks.FirstOrDefault(attack => attack.id == id);

            if (attack == null) return StatusCode(404, new { success = false, nessage = "attack not found" });
            if (attack.status == "Completed")
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
            _context.Update(attack);
            await _context.SaveChangesAsync();
            return StatusCode(
                StatusCodes.Status200OK,
                 new { message = "attack started.", TaskId = attackTask.Id });
        }

        // POST: intercept missile
        [HttpPost("{id}/intercept")]
        [Produces("Application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> InterceptMissile(Guid id)
        {

            Attack attack = await _context.Attacks.FirstOrDefaultAsync(attack => attack.id == id);
            if (attack == null) return NotFound();
            if (attack.status == "Completed")
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
            _context.Update(attack);
            await _context.SaveChangesAsync();
            return StatusCode(
                StatusCodes.Status200OK,
                 new
                 {
                     message = "Attack intercepted.",
                     status = "Success"
                 });
        }

    }
}
