using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using pro.Models;
using pro.DTOs;
using pro.Data;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using pro.DTOs.Inside;

namespace pro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyUserController : ControllerBase
    {
        private readonly Context _context; // Database access
        private readonly UserManager<User> _userManager; // Register user

        public CompanyUserController(Context context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        [HttpPost("company")]
        public async Task<ActionResult<Company>> CreateCompanyForUser(CompanyDTO companyDTO)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    return NotFound("User not found.");
                }

                if (user.Company != null)
                {
                    return BadRequest("User already has an associated company.");
                }

                var newCompany = new Company
                {
                    UserId = userId,
                    CompanyName = companyDTO.CompanyName,
                    Description = companyDTO.Description,
                    StartDate = companyDTO.StartDate,
                    EndDate = companyDTO.EndDate
                };

                user.Company = newCompany;

                await _context.SaveChangesAsync();

                return Ok(newCompany);
            }
            catch (Exception ex)
            {
                // Log the exception if needed

                // Set HTTP status code to 500 (Internal Server Error)
                return StatusCode(500, "Error creating company");
            }
        }
    }
}
