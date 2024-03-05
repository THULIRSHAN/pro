using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pro.Data;
using pro.DTOs;
using pro.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
 // Restrict access to Admin role only
public class AdminController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly Context _context; // Assuming you have a DbContext named "Context" to access the database
 

    public AdminController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, Context context)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
    }

    // ... (Other endpoints)
    [HttpGet("get_app")]
    public async Task<ActionResult<UserApplicantDto>> GetApplicantByPhoneNumber(string phoneNumber)
    {
        // Check if the phoneNumber is null or empty
        if (string.IsNullOrEmpty(phoneNumber))
        {
            return BadRequest("Phone number cannot be empty.");
        }

        // Find the applicant with the provided phoneNumber
        var applicant = await _context.Applicants.FirstOrDefaultAsync(a => a.PhoneNo == phoneNumber);
        if (applicant == null)
        {
            return NotFound("Applicant not found.");
        }

        // Get the associated user data for the applicant
        var user = await _userManager.FindByIdAsync(applicant.UserId);
        if (user == null)
        {
            return NotFound("User not found.");
        }

        // Create the DTO to return the user's applicant details
        var userApplicantDto = new UserApplicantDto
        {
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Applicant = applicant
        };

        return Ok(userApplicantDto);
    }
    
   


}
