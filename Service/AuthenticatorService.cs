using IA_AbansiBabayiSystemBlazor.Data.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IA_AbansiBabayiSystemBlazor.Services
{
    public class AuthenticatorService
    {
        private readonly AuthenticationStateProvider _authProvider;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthenticatorService(AuthenticationStateProvider authProvider, UserManager<ApplicationUser> userManager)
        {
            _authProvider = authProvider;
            _userManager = userManager;
        }

        // Method 1: Get current user
        public async Task<ApplicationUser> GetCurrentUserAsync()
        {
            var authState = await _authProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (!user.Identity.IsAuthenticated)
                return null;

            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return null;

            return await _userManager.FindByIdAsync(userId);
        }

        // Method 2: Check if user has any of these roles
        public async Task<bool> UserHasRoleAsync(params string[] roles)
        {
            var authState = await _authProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            return roles.Any(role => user.IsInRole(role));
        }

        // Method 3: Get user ID
        public async Task<string> GetUserIdAsync()
        {
            var authState = await _authProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        // Method 4: Check if user is authenticated
        public async Task<bool> IsAuthenticatedAsync()
        {
            var authState = await _authProvider.GetAuthenticationStateAsync();
            return authState.User.Identity.IsAuthenticated;
        }
    }
}