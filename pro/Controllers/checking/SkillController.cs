using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using pro.Data;
using pro.DTOs;
using pro.DTOs.Inside;
using pro.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace pro.Controllers.checking
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

        [HttpGet("GetSkills")]
        public ActionResult<IEnumerable<Skill>> GetSkills()
        {
            // Retrieve all skills from the database
            var skills = _context.Skills.ToList();

            // Return the list of skills
            return Ok(skills);
        }


        [HttpPost("CreateSkill")]
        public async Task<ActionResult<Skill>> CreateSkill(SkillDto skillDto)
        {
            var newSkill = new Skill
            {
                SkillType = skillDto.SkillType,
                SkillName = skillDto.SkillName,
            };

            // Add the new skill to the context
            _context.Skills.Add(newSkill);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return the newly created skill
            return Ok(newSkill);
        }

        [HttpGet("GetSkillsByType/{type}")]
        public ActionResult<IEnumerable<Skill>> GetSkillsByType(int type)
        {
            // Retrieve skills from the database based on type
            var skills = _context.Skills.Where(skill => skill.SkillType == type).ToList();

            // Return the filtered list of skills
            return Ok(skills);
        }
        [HttpDelete("DeleteSkill/{id}")]
        public async Task<ActionResult> DeleteSkill(int id)
        {
            // Find the skill in the database based on its ID
            var skillToDelete = await _context.Skills.FindAsync(id);

            // If skill not found, return NotFound
            if (skillToDelete == null)
            {
                return NotFound();
            }

            // Remove the skill from the context
            _context.Skills.Remove(skillToDelete);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return Ok if deletion is successful
            return Ok();
        }

        [HttpPut("UpdateSkill/{id}")]
        public async Task<ActionResult<Skill>> UpdateSkill(int id, SkillDto updatedSkillDto)
        {
            // Find the skill in the database based on its ID
            var skillToUpdate = await _context.Skills.FindAsync(id);

            // If skill not found, return NotFound
            if (skillToUpdate == null)
            {
                return NotFound();
            }

            // Update the skill properties
            skillToUpdate.SkillType = updatedSkillDto.SkillType;
            skillToUpdate.SkillName = updatedSkillDto.SkillName;

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return the updated skill
            return Ok(skillToUpdate);
        }


    }
}
