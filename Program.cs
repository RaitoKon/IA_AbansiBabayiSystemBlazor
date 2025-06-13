using IA_AbansiBabayiSystemBlazor.Components;
using IA_AbansiBabayiSystemBlazor.Data;
using IA_AbansiBabayiSystemBlazor.Hubs;
using IA_AbansiBabayiSystemBlazor.Service;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Use custom authentication state provider
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

// Add authorization support for Blazor components
builder.Services.AddAuthorizationCore();

// Database connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")

    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Developer exception page for DB errors
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Custom session management service
builder.Services.AddScoped<UserSessionService>();

// Passed Data Route Service
builder.Services.AddScoped<PassedDataRoute>();

builder.Services.AddMudServices();

builder.Services.AddSignalR();
builder.Services.AddSingleton(typeof(TableDataService<>));


var dbContextType = typeof(ApplicationDbContext);
var dbSetProperties = dbContextType.GetProperties()
    .Where(p => p.PropertyType.IsGenericType &&
                p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));

foreach (var dbSetProp in dbSetProperties)
{
    var entityType = dbSetProp.PropertyType.GetGenericArguments()[0];

    // Register TableDataService<T>
    var serviceType = typeof(TableDataService<>).MakeGenericType(entityType);
    builder.Services.AddScoped(serviceType);
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.MapHub<AutoUpdateHub>("/autoupdatehub");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// **Add UseAuthorization middleware to enable policy enforcement**
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
