using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using IA_AbansiBabayiSystemBlazor.Data.Models;
using static IA_AbansiBabayiSystemBlazor.Components.Pages.LandingPage;

namespace IA_AbansiBabayiSystemBlazor.Controllers
{

    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] string email, [FromForm] string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
                return BadRequest(new { message = "Invalid credentials." });

            await _signInManager.SignInAsync(user, isPersistent: false);

            if (user.MustChangePassword)
                return Ok(new { redirectUrl = "/forceResetPasswordPage" });

            return Ok(new { redirectUrl = "/userPage" });
        }


        private ResetLoginModel resetLoginModel = new();

        [HttpPost("reset-login")]
        public async Task<IActionResult> ResetLogin([FromBody] ResetLoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
                return BadRequest("User not found.");

            // Ensure the user has a password (especially if one was just set)
            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
                return BadRequest("User has no password set.");

            // Sign out the current session
            await _signInManager.SignOutAsync();

            // Attempt to log in with the new password
            var result = await _signInManager.PasswordSignInAsync(user.UserName, model.NewPassword, isPersistent: false, lockoutOnFailure: false);
            if (!result.Succeeded)
                return BadRequest("Login failed after password reset.");

            return Ok(); // Successful login
        }

    }
}


