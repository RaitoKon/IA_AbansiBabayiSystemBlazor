using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly IJSRuntime _js;

    public CustomAuthStateProvider(IJSRuntime js)
    {
        _js = js;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsIdentity identity;

        try
        {
            var userId = await _js.InvokeAsync<string>("sessionStorage.getItem", "userId");
            var email = await _js.InvokeAsync<string>("sessionStorage.getItem", "userEmail");
            var role = await _js.InvokeAsync<string>("sessionStorage.getItem", "userRole");

            if (!string.IsNullOrEmpty(userId))
            {
                identity = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, email ?? ""),
                new Claim(ClaimTypes.Role, role ?? "")
            }, "CustomAuth");
            }
            else
            {
                identity = new ClaimsIdentity(); // Anonymous
            }
        }
        catch (InvalidOperationException)
        {
            // JS interop not available (e.g., during prerendering)
            identity = new ClaimsIdentity(); // Treat as anonymous
        }

        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    // Optional: call this when login state changes
    public void NotifyUserAuthentication(string userId, string email, string role)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Name, email),
            new Claim(ClaimTypes.Role, role)
        };
        var identity = new ClaimsIdentity(claims, "CustomAuth");
        var user = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public void NotifyUserLogout()
    {
        var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
    }
}
