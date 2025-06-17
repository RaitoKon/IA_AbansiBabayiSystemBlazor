using IA_AbansiBabayiSystemBlazor.Components;
using IA_AbansiBabayiSystemBlazor.Data;
using IA_AbansiBabayiSystemBlazor.Data.Models;
using IA_AbansiBabayiSystemBlazor.Hubs;
using IA_AbansiBabayiSystemBlazor.Service;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// --- Razor Components ---
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// --- Database Setup ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// --- Data Services per Entity ---
var dbContextType = typeof(ApplicationDbContext);
var dbSetProperties = dbContextType.GetProperties()
    .Where(p => p.PropertyType.IsGenericType &&
                p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));

foreach (var dbSetProp in dbSetProperties)
{
    var entityType = dbSetProp.PropertyType.GetGenericArguments()[0];
    var serviceType = typeof(TableDataService<>).MakeGenericType(entityType);
    builder.Services.AddScoped(serviceType);
}

// --- General Services ---
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddScoped<PassedDataRoute>();
builder.Services.AddMudServices();
builder.Services.AddSignalR();
builder.Services.AddSingleton(typeof(TableDataService<>));
builder.Services.AddAuthorization();

// --- Email, Http, Controllers ---
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7106") });
builder.Services.AddScoped<EmailService>();
builder.Services.AddControllers();

// --- Identity (Minimal for Now) ---
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// --- Authentication State (default, clean) ---
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<ApplicationUser>>();
builder.Services.AddCascadingAuthenticationState();

// --- Optional: Configure login cookie ---
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/landingPage";
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Lax;
});

var app = builder.Build();

// --- Role Seeder (optional if you plan to add roles) ---
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await SeedRoles.InitializeAsync(roleManager);
}

// --- Middleware ---
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
