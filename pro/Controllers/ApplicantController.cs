using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using pro.Models;
using pro.DTOs;
using pro.Data;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace pro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicantController : ControllerBase
    {
        private readonly Context _context;//database access
        private readonly UserManager<User> _userManager;//register user

        public ApplicantController(Context context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        [HttpPost("app")]
        public async Task<ActionResult<Applicant>> CreateApplicantForUser(ApplicantDTO applicantDto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    return NotFound("User not found.");
                }

                if (user.Applicant != null)
                {
                    return BadRequest("User already has an associated Applicant.");
                }

                var newApplicant = new Applicant
                {
                    UserId = userId,
                    Title = applicantDto.Title,
                    Dob = applicantDto.Dob,
                    Gender = applicantDto.Gender,
                    PhoneNo = applicantDto.PhoneNo,
                    Email = applicantDto.Email,
                    Address = applicantDto.Address,
                    Street = applicantDto.Street,
                    City = applicantDto.City,
                    State = applicantDto.State,
                    Zip = applicantDto.Zip,
                    Country = applicantDto.Country,
                };

                user.Applicant = newApplicant;

                await _context.SaveChangesAsync();

                return Ok(newApplicant);
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, "Duplicate entry");
            }
        }

        //[Authorize]
        [HttpGet("app")]
        public async Task<ActionResult<ApplicantDTO>> GetApplicantForUser()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    return NotFound("User not found.");
                }

                if (user.Applicant == null)
                {
                    return NotFound("Applicant not found for the user.");
                }

                var applicantDto = new ApplicantDTO
                {
                    Title = user.Applicant.Title,
                    Dob = user.Applicant.Dob,
                    Gender = user.Applicant.Gender,
                    PhoneNo = user.Applicant.PhoneNo,
                    Email = user.Applicant.Email,
                    Address = user.Applicant.Address,
                    Street = user.Applicant.Street,
                    City = user.Applicant.City,
                    State = user.Applicant.State,
                    Zip = user.Applicant.Zip,
                    Country = user.Applicant.Country,
                };

                return Ok(applicantDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [Authorize]
        [HttpPut("UpdateApplicant")]
        public async Task<ActionResult<Applicant>> UpdateApplicant(ApplicantDTO updatedApplicantDto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null || user.Applicant == null)
                {
                    return NotFound("User or Applicant not found.");
                }

                // Update the applicant properties
                user.Applicant.Title = updatedApplicantDto.Title;
                user.Applicant.Dob = updatedApplicantDto.Dob;
                user.Applicant.Gender = updatedApplicantDto.Gender;
                user.Applicant.PhoneNo = updatedApplicantDto.PhoneNo;
                user.Applicant.Email = updatedApplicantDto.Email;
                user.Applicant.Address = updatedApplicantDto.Address;
                user.Applicant.Street = updatedApplicantDto.Street;
                user.Applicant.City = updatedApplicantDto.City;
                user.Applicant.State = updatedApplicantDto.State;
                user.Applicant.Zip = updatedApplicantDto.Zip;
                user.Applicant.Country = updatedApplicantDto.Country;

                await _context.SaveChangesAsync();

                return Ok(user.Applicant);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [Authorize]
        [HttpDelete("DeleteApplicant")]
        public async Task<ActionResult> DeleteApplicant()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null || user.Applicant == null)
                {
                    return NotFound("User or Applicant not found.");
                }

                // Remove the applicant from the user
                user.Applicant = null;

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }


    }
}
