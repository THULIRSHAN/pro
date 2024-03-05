
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pro.Data;
using pro.DTOs.Inside;
using pro.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace pro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EducationController : ControllerBase
    {
        private readonly Context _context;
        private readonly UserManager<User> _userManager; // Add this line to include UserManager
        private readonly ILogger<EducationController> _logger;

        public EducationController(Context context, UserManager<User> userManager, ILogger<EducationController> logger) // Add UserManager to the constructor
        {
            _context = context;
            _userManager = userManager; // Assign UserManager in the constructor
            _logger = logger;
        }

        [HttpPost("app")]
        public async Task<ActionResult<Education>> CreateApplicantForUser(Edu edu)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var newEducation = new Education
            {
                UserId = userId,
                CurrentStatus = edu.EducationDTO.CurrentStatus,
                Qulification = edu.EducationDTO.Qulification,
                InsituteName = edu.EducationDTO.InsituteName,
                Yearattained = edu.EducationDTO.Yearattained,
            };

            var softSkillIds = GetSkillIdsByNames(edu.SkillUserDTO.SoftSkill);
            var hardSkillIds = GetSkillIdsByNames(edu.SkillUserDTO.HardSkill);
            var languageIds = GetSkillIdsByNames(edu.SkillUserDTO.Language);

            var skillUsers = new List<SkillUser>();

            foreach (var skillId in softSkillIds)
            {
                var newSkillUser = new SkillUser
                {
                    UserId = userId,
                    Skillid = skillId,
                };
                skillUsers.Add(newSkillUser);
            }

            foreach (var skillId in hardSkillIds)
            {
                var newSkillUser = new SkillUser
                {
                    UserId = userId,
                    Skillid = skillId,
                };
                skillUsers.Add(newSkillUser);
            }

            foreach (var skillId in languageIds)
            {
                var newSkillUser = new SkillUser
                {
                    UserId = userId,
                    Skillid = skillId,
                };
                skillUsers.Add(newSkillUser);
            }

            var departmentId = GetDepartmentIdByName(edu.DepartmentUserDTO.DepartmentID);

            var existingDepartmentUser = _context.DepartmentUsers
                .FirstOrDefault(d => d.UserId == userId && d.DepartmentID == departmentId);

            if (existingDepartmentUser != null)
            {
                return BadRequest("User is already associated with this department.");
            }
            else
            {
                var newDepartmentUser = new DepartmentUser
                {
                    UserId = userId,
                    DepartmentID = departmentId,
                };

                _context.SkillUsers.AddRange(skillUsers);
                _context.DepartmentUsers.Add(newDepartmentUser);

                user.Educations = new List<Education> { newEducation };
                user.SkillUsers = skillUsers;
                user.DepartmentUsers = new List<DepartmentUser> { newDepartmentUser };

                await _context.SaveChangesAsync();

                return Ok(newEducation);
            }




        }

        private List<int> GetSkillIdsByNames(List<int> skillNames)
        {
            // Assuming your Skill table is named "_context.Skills" and has SkillId and SkillName properties
            return _context.Skills
                .Where(skill => skillNames.Contains(skill.Skillid))
                .Select(skill => skill.Skillid)
                .ToList();
        }

        private int GetDepartmentIdByName(int departmentId)
        {
            var department = _context.Departments.FirstOrDefault(d => d.DepartmentID == departmentId);
            return department?.DepartmentID ?? 0; // Return 0 if not found or actual department ID
        }





    }
}
