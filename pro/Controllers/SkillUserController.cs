//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Newtonsoft.Json.Linq;
//using pro.Data;
//using pro.DTOs.Inside;
//using pro.Models;
//using System;
//using System.Linq;
//using System.Security.Claims;
//using System.Text.Json.Nodes;
//using System.Threading.Tasks;

//namespace pro.Controllers.Admin
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class SkillUserController : ControllerBase
//    {
//        private readonly Context _context;
//        private readonly UserManager<User> _userManager;

//        public SkillUserController(Context context, UserManager<User> userManager)
//        {
//            _context = context;
//            _userManager = userManager;
//        }

//        [Authorize]
//        [HttpPost("AddUserToSkill")]
//        public async Task<ActionResult> AddUserToSkill(SkillUserDTO skillUserDTO)
//        {
//            ////JObject jsonObject = skillUserDTO;
//            //JsonArray fruitsArray = (JsonArray)skillUserDTO["language"];

//            //string name = "";

//            //foreach (var fruit in fruitsArray)
//            //{
//            //    name = fruit.ToString();
//            //    Console.WriteLine(fruit);

//            //}
//            //return Ok(name);


//            // Get the authenticated user's UserId from the ClaimsPrincipal
//            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

//            // Check if the user exists in the database
//            var user = await _userManager.FindByIdAsync(userId);
//            if (user == null)
//            {
//                return NotFound("User not found.");
//            }

//            // Find the skill by its name
//             var skill = await _context.Skills.FirstOrDefaultAsync(s => s.SkillName == skillUserDTO.SoftSkill);
//            if (skill == null)
//            {
//                return NotFound("Skill not found.");
//            }

//            // Add the user to the SkillUser table
//            var skillUser = new SkillUser
//            {
//                UserId = user.Id,
//                Skillid = Skill.Skillid
//            };

//            _context.Skillusers.Add(skillUser);
//            await _context.SaveChangesAsync();
//            Console.WriteLine(skillUserDTO);

//            return Ok("User added to skill successfully.");

//            return Ok(skillUserDTO);
//        }

//        // Add other actions for updating, deleting, and retrieving skill user assignments
//    }
//}
