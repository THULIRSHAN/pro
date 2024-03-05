using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using pro.Data;
using pro.DTOs.Inside;
using pro.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace pro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobApplicationController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<Models.User> _userManager;

        public JobApplicationController(Context context, UserManager<Models.User> userManager) 
        {
            _context = context;
            _userManager = userManager; // Assign UserManager in the constructor

        }
        [HttpPost("Job")]
        public async Task<ActionResult<JobApplication>> CreateJobApplication(JobApplicationDTO jobApplicationDTO)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null){
                return BadRequest("user is not found");
            }

            var newJobApplication = new JobApplication()
            {
                UserId= userId,
                DesiredLocation= jobApplicationDTO.DesiredLocation,
                IsFullTimePosition= jobApplicationDTO.IsFullTimePosition,
                StartDate= jobApplicationDTO.StartDate,
                Source= jobApplicationDTO.Source,
                PreferredContactMethod= jobApplicationDTO.PreferredContactMethod,

            };

            user.JobApplications = new List<JobApplication> { newJobApplication };
            await _context.SaveChangesAsync();

            return Ok(newJobApplication);

        }

    }
}
