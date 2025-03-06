using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MerchIndex.Auto.Client.Services;
using MerchIndex.Auto.Components.Account;
using MerchIndex.Auto.Components.Pages.Demos.StateContainer;
using MerchIndex.Auto.Data;
using MerchIndex.Auto.Repository;
using MerchIndex.Auto.Repository.IRepository;
using MerchIndex.Auto.Services;
using MerchIndex.Auto.Components;
using Microsoft.AspNetCore.Authentication.Cookies;
using ServiceCollectionExtensions;
using Persilsoft.Nominatim.Geolocation.Blazor;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents().AddHubOptions(o => o.MaximumReceiveMessageSize = 100_000_000)
    .AddInteractiveWebAssemblyComponents()
    .AddAuthenticationStateSerialization();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();
//builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(opts =>
{
    opts.UseSqlServer(connectionString);
    opts.EnableSensitiveDataLogging(true);
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddUserManager<UserManager<ApplicationUser>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 1;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;

    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;

    options.Lockout.AllowedForNewUsers = false;
    options.Lockout.MaxFailedAccessAttempts = 10;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);

    options.User.RequireUniqueEmail = true;
    //options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
});

//builder.Services.Configure<CookieAuthenticationOptions>(
// IdentityConstants.ApplicationScheme,
// opts => {
//     opts.LoginPath = "/login";
//     opts.AccessDeniedPath = "/NotAllowed";
// });

builder.Services.AddQuickGridEntityFrameworkAdapter();

builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString),
    ServiceLifetime.Transient);

//https://learn.microsoft.com/en-us/aspnet/core/blazor/globalization-localization?view=aspnetcore-9.0
//builder.Services.AddLocalization();

// register server-based implementation to integrate with an API
builder.Services.AddScoped<IApiService, ServerApiService>();

builder.Services.AddHttpClient("LocalAPIClient", cfg =>
{
    cfg.BaseAddress = new Uri(
        builder.Configuration["LocalAPIBaseAddress"] ??
            throw new Exception("LocalAPIBaseAddress is missing."));
});
builder.Services.AddHttpClient("RemoteAPIClient", cfg =>
{
    cfg.BaseAddress = new Uri(
       builder.Configuration["RemoteAPIBaseAddress"] ??
           throw new Exception("RemoteAPIBaseAddress is missing."));
});
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7228/api/") });
//builder.Services.AddHttpClient();

builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();
builder.Services.AddScoped<IOrderRepository, EFOrderRepository>();

builder.Services.AddScoped<LocalStorageService>();
builder.Services.AddScoped<CartService, ProtectedLocalStorageCartService>();
builder.Services.AddScoped<StateContainer>();
builder.Services.AddScoped<ImageService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<FileService>();
builder.Services.AddScoped<ExcelService>();

builder.Services.AddGeolocationService();
builder.Services.AddNominatimGeocoderService();
builder.Services.AddScoped<GeolocationService>();
builder.Services.AddScoped<SmsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    //app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    //app.UseMigrationsEndPoint();
}
app.UseMigrationsEndPoint();

//app.UseRequestLocalization(options =>
//{
//    options.AddSupportedCultures("en-US", "sv-SE")
//    .AddSupportedUICultures("en-US", "sv-SE")
//    .SetDefaultCulture("sv-SE");
//});
//app.UseRequestLocalization(new RequestLocalizationOptions()
//.AddSupportedCultures(new[] { "en-US", "sv-SE" })
//.AddSupportedUICultures(new[] { "en-US", "sv-SE" })
//.SetDefaultCulture("sv-SE"));
//app.UseRequestLocalization(options =>
//{
//    options.AddSupportedCultures("en-US").AddSupportedUICultures("en-US").SetDefaultCulture("en-US");
//});

app.UseHttpsRedirection();

app.UseStaticFiles();
app.MapStaticAssets();

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapControllers();
app.MapControllerRoute("controllers",
 "controllers/{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

//https://github.com/dotnet/aspnetcore/issues/52063
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(MerchIndex.Auto.Client._Imports).Assembly)
    .AllowAnonymous();

// define a local API for testing
app.MapGet("localapi/bands", () =>
{
    return Results.Ok(new[] { new { Id = 1, Name = "Nirvana (from local API)" },
        new { Id = 2, Name = "Queens of the Stone Age (from local API)" },
        new { Id = 3, Name = "Fred Again. (from local API)" },
        new { Id = 4, Name = "Underworld (from local API)" } });
});

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
if (context.Database.GetPendingMigrations().Any())
{
    context.Database.Migrate();
}

IdentitySeedData.EnsurePopulated(app.Services, app.Configuration);
SeedData.EnsurePopulated(context);

app.Run();
