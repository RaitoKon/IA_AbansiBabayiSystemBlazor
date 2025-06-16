using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IA_AbansiBabayiSystemBlazor.Service
{

    public class RevalidatingIdentityAuthenticationStateProvider<TUser> :
        RevalidatingServerAuthenticationStateProvider where TUser : class
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IdentityOptions _options;
        private readonly ILogger _logger;

        public RevalidatingIdentityAuthenticationStateProvider(
            ILoggerFactory loggerFactory,
            IServiceScopeFactory scopeFactory,
            IOptions<IdentityOptions> optionsAccessor)
            : base(loggerFactory)
        {
            _scopeFactory = scopeFactory;
            _options = optionsAccessor.Value;
            _logger = loggerFactory.CreateLogger<RevalidatingIdentityAuthenticationStateProvider<TUser>>();
        }

        protected override TimeSpan RevalidationInterval => TimeSpan.FromMinutes(30);

        protected override async Task<bool> ValidateAuthenticationStateAsync(
            AuthenticationState authenticationState, CancellationToken cancellationToken)
        {
            var user = authenticationState.User;
            if (user.Identity is not { IsAuthenticated: true })
            {
                return false;
            }

            using var scope = _scopeFactory.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<TUser>>();
            var signInManager = scope.ServiceProvider.GetRequiredService<SignInManager<TUser>>();
            var userId = userManager.GetUserId(user);
            var reloadedUser = await userManager.FindByIdAsync(userId);
            if (reloadedUser == null)
            {
                _logger.LogInformation("User not found during revalidation.");
                return false;
            }

            return await signInManager.CanSignInAsync(reloadedUser);
        }
    }

}
