using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pro.Data;
using pro.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace pro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Require authorization for all endpoints in this controller
    public class AcknowledgmentController : ControllerBase
    {
        private readonly Context _context;
        private readonly UserManager<User> _userManager;

        public AcknowledgmentController(Context context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult<Acknowledgment>> CreateAcknowledgment(Acknowledgment acknowledgment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Get authenticated user's ID
            var user = await _userManager.FindByIdAsync(userId); // Get user from UserManager

            if (user == null)
            {
                return NotFound("User not found.");
            }

            acknowledgment.UserId = userId; // Associate acknowledgment with the authenticated user

            _context.Acknowledgments.Add(acknowledgment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAcknowledgment), new { id = acknowledgment.AckId }, acknowledgment);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Acknowledgment>> GetAcknowledgment(int id)
        {
            var acknowledgment = await _context.Acknowledgments.FindAsync(id);

            if (acknowledgment == null)
            {
                return NotFound();
            }

            return acknowledgment;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAcknowledgment(int id, Acknowledgment acknowledgment)
        {
            if (id != acknowledgment.AckId)
            {
                return BadRequest();
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Get authenticated user's ID
            var user = await _userManager.FindByIdAsync(userId); // Get user from UserManager

            if (user == null)
            {
                return NotFound("User not found.");
            }

            if (acknowledgment.UserId != userId)
            {
                return Unauthorized(); // User is not authorized to update this acknowledgment
            }

            _context.Entry(acknowledgment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AcknowledgmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAcknowledgment(int id)
        {
            var acknowledgment = await _context.Acknowledgments.FindAsync(id);
            if (acknowledgment == null)
            {
                return NotFound();
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Get authenticated user's ID
            var user = await _userManager.FindByIdAsync(userId); // Get user from UserManager

            if (user == null)
            {
                return NotFound("User not found.");
            }

            if (acknowledgment.UserId != userId)
            {
                return Unauthorized(); // User is not authorized to delete this acknowledgment
            }

            _context.Acknowledgments.Remove(acknowledgment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AcknowledgmentExists(int id)
        {
            return _context.Acknowledgments.Any(e => e.AckId == id);
        }
    }
}
