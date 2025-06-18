using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using IA_AbansiBabayiSystemBlazor.Data.Models;
using MudBlazor.Interfaces;

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
        public async Task<IActionResult> RedirectLogin([FromForm] string email, [FromForm] string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
                return Unauthorized();

            await _signInManager.SignInAsync(user, isPersistent: false);

            if (user.MustChangePassword)
                return Redirect("/forceResetPasswordPage");

            return Redirect("/userPage");
        }

        [HttpPost("reset-login")]
        public async Task<IActionResult> ResetLogin([FromBody] ResetLoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
                return BadRequest("User not found.");

            await _signInManager.SignOutAsync();

            var result = await _signInManager.PasswordSignInAsync(user.UserName, model.NewPassword, isPersistent: false, lockoutOnFailure: false);
            if (!result.Succeeded)
                return BadRequest("Login failed after password reset.");

            return Ok(); // Successful login
        }

        public class ResetLoginModel
        {
            public string Username { get; set; }
            public string NewPassword { get; set; }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}
