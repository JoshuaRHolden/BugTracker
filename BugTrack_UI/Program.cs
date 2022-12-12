using BugTrack_UI.Areas.Identity;
using BugTrack_UI.Context;
using BugTrack_UI.Services;
using Fluxor;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

IConfigurationBuilder configBuilder = new ConfigurationBuilder()
       .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
IConfiguration config = configBuilder.Build();
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<Data.Models.ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddQuickGridEntityFrameworkAdapter();
builder.Services.AddRazorPages();
builder.WebHost.UseStaticWebAssets();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<Data.Models.ApplicationUser>>();
builder.Services.AddScoped<IHttpService,HttpService>();
builder.Services.AddFluxor(o => o.ScanAssemblies(typeof(Program).Assembly).UseReduxDevTools());
builder.WebHost.UseKestrel(serverOptions =>
{
    serverOptions.ListenLocalhost(5057);
    serverOptions.ListenLocalhost(7041, listenOptions => listenOptions.UseHttps());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.UseForwardedHeaders();

app.Run();