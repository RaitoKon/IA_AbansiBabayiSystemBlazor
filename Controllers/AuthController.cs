using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using IA_AbansiBabayiSystemBlazor.Data.Models;
using IA_AbansiBabayiSystemBlazor.Service;
using Microsoft.EntityFrameworkCore; // Add for Include extension
using System.Linq; // Add for FirstOrDefault

namespace IA_AbansiBabayiSystemBlazor.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TableDataService<TroopLeader> _troopLeaderService;
        private readonly TableDataService<TroopMember> _troopMemberService;

        public AuthController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            TableDataService<TroopLeader> troopLeaderService,
            TableDataService<TroopMember> troopMemberService )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _troopLeaderService = troopLeaderService;
            _troopMemberService = troopMemberService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> RedirectLogin([FromForm] string email, [FromForm] string password)
        {
            try
            {
                Console.WriteLine($"Login attempt for email: {email}");

                await _troopLeaderService.LoadDataAsync(query =>
                    query.Include(t => t.ApplicationUser)
                );

                await _troopMemberService.LoadDataAsync(query =>
                    query.Include(t => t.ApplicationUser)
                );

                var troopLeader = _troopLeaderService.Data
                    .FirstOrDefault(t =>
                        (!string.IsNullOrEmpty(t.LeaderRegisteredEmail) && t.LeaderRegisteredEmail == email));

                var troopMember = _troopMemberService.Data
                    .FirstOrDefault(t =>
                        (!string.IsNullOrEmpty(t.TroopMemRegisteredEmail) && t.TroopMemRegisteredEmail == email));

                if (troopLeader?.ApplicationUser == null && troopMember?.ApplicationUser == null)
                {
                    Console.WriteLine("No application user found");
                    return BadRequest(new { error = "Invalid credentials" });
                }

                var user = troopLeader?.ApplicationUser ?? troopMember?.ApplicationUser;

                if (user.AccountStatusId != 2)
                {
                    return BadRequest(new { error = "Account is not active" });
                }

                var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
                Console.WriteLine($"Password valid: {isPasswordValid}");

                if (!isPasswordValid)
                {
                    return BadRequest(new { error = "Invalid credentials" });
                }

                await _signInManager.SignInAsync(user, isPersistent: false);
                Console.WriteLine("Sign in successful");

                if (user.MustChangePassword)
                {
                    Console.WriteLine("Redirecting to password reset");
                    return Ok(new { redirectUrl = "/forceResetPasswordPage" });
                }

                Console.WriteLine("Redirecting to user page");
                return Ok(new { redirectUrl = "/userPage" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login error: {ex}");
                return BadRequest(new { error = "An unexpected error occurred" });
            }
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

            return Ok();
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