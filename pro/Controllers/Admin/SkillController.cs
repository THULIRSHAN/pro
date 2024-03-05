using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pro.Data;
using pro.DTOs.Inside;
using pro.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly Context _context;

        public SkillController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkillDto>>> GetSkills()
        {
            var skills = await _context.Skills
                .Select(s => new SkillDto
                {
                    SkillType = s.SkillType,
                    SkillName = s.SkillName
                })
                .ToListAsync();

            return Ok(skills);
        }

        [HttpPost]
        public async Task<ActionResult<Skill>> CreateSkill(SkillDto skillDto)
        {
            var skill = new Skill
            {
                SkillType = skillDto.SkillType,
                SkillName = skillDto.SkillName
            };

            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSkill), new { id = skill.Skillid }, skill);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SkillDto>> GetSkill(int id)
        {
            var skill = await _context.Skills
                .Where(s => s.Skillid == id)
                .Select(s => new SkillDto
                {
                    SkillType = s.SkillType,
                    SkillName = s.SkillName
                })
                .FirstOrDefaultAsync();

            if (skill == null)
            {
                return NotFound();
            }

            return Ok(skill);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSkill(int id, SkillDto skillDto)
        {
            var skill = await _context.Skills.FindAsync(id);

            if (skill == null)
            {
                return NotFound();
            }

            skill.SkillType = skillDto.SkillType;
            skill.SkillName = skillDto.SkillName;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkill(int id)
        {
            var skill = await _context.Skills.FindAsync(id);

            if (skill == null)
            {
                return NotFound();
            }

            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
