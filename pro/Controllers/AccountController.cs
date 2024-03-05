using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using pro.DTOs.Account;
using pro.Models;
using System;
using System.Threading.Tasks;

namespace pro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                DateCreated = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok(new { message = "Registration successful" });
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                // Log the username to check if it's being retrieved correctly
                Console.WriteLine($"User '{model.UserName}' not found");
                return Unauthorized("Invalid username or password");
            }

            // Log the retrieved user details for debugging
            Console.WriteLine($"Found user: {user.UserName}, {user.Email}");

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (result.Succeeded)
            {
                return Ok(new { message = "Login successful" });
            }
            else
            {
                // Log failed login attempts for troubleshooting
                Console.WriteLine($"Login failed for user '{user.UserName}'");
                return Unauthorized("Invalid username or password");
            }
        }


        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, model.NewPassword);

            if (result.Succeeded)
            {
                return Ok(new { message = "Password reset successful" });
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
    }
}
