using BugTrack_UI.Areas.Identity;
using BugTrack_UI.Context;
using BugTrack_UI.Pages;
using Bunit;
using Bunit.TestDoubles;
using Fluxor;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace BUGTRACK_UI_Tests
{
    public class UITests : TestContext
    {
        TestContext tc = new TestContext();
        TestAuthorizationContext AuthContext;

        public UITests()
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();
            tc.Services.AddSingleton<IConfiguration>(config);
            tc.Services.AddDefaultIdentity<Data.Models.ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ApplicationDbContext>();
            tc.Services.AddFluxor(o => o.ScanAssemblies(typeof(Program).Assembly).UseReduxDevTools());
            tc.Services.AddHttpClient();
            tc.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<Data.Models.ApplicationUser>>();
            tc.Services.AddServerSideBlazor();
            tc.Services.AddQuickGridEntityFrameworkAdapter();
            tc.Services.AddRazorPages();
       
            var testHostingEnvironment = new MockHostingEnvironment();
           tc.Services.AddSingleton<IWebHostEnvironment, MockHostingEnvironment>();
            var navMan = tc.Services.GetRequiredService<FakeNavigationManager>();
            navMan.NavigateToLogin("/");



        }
        [Fact]
        public void Test1()
        {
            tc.RenderComponent<AllBugs>();
            
        }

        public class MockHostingEnvironment : IWebHostEnvironment
        {
           
            public string ApplicationName { get => "BugTrack"; set => throw new NotImplementedException(); }
            public string ContentRootPath { get => "C:\\Users\\joshu\\source\\repos\\BugTracker\\BugTrack_UI"; set => throw new NotImplementedException(); }
            public IFileProvider ContentRootFileProvider { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public string WebRootPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public IFileProvider WebRootFileProvider { get => new CompositeFileProvider(new IFileProvider[0]); set => throw new NotImplementedException(); }
            public string EnvironmentName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        }
    }
}