using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Data.Entities;
using WebAPI.Hubs;
using WebAPI.Twilioo;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddControllers();
            services.AddSignalR();
            services.AddScoped<TwilioWhatsAppService>();
            services.Configure<TwilioOptions>(settings =>
            {
                settings.AccountSid = Configuration["AzureKeyVaultTwilioAccountSID"];
                settings.ApiSecret = Configuration["AzureKeyVaultTwilioAPISecret"];
                settings.ApiKey = Configuration["AzureKeyVaultTwilioAPIKey"];
                settings.AuthToken = Configuration["AzureKeyVaultTwilioAuthToken"];
            });

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration["AzureKeyVaultAzureSQLConnection"]);
            });

            AuthenticationBuilder authenticationBuilder = services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddGoogle(options =>
            {
                options.ClientId = Configuration["AzureKeyVaultGoogleClientId"];
                options.ClientSecret = Configuration["AzureKeyVaultGoogleClientSecret"];
                options.Events.OnCreatingTicket = (context) =>
                {
                    var picture = context.User.GetProperty("picture").GetString();
                    context.Identity.AddClaim(new Claim("picture", picture));
                    return Task.CompletedTask;
                };
            })
            .AddGitHub(options =>
            {
                options.ClientId = Configuration["AzureKeyVaultGitHubClientId"];
                options.ClientSecret = Configuration["AzureKeyVaultGitHubClientSecret"];
                options.Events.OnCreatingTicket = context =>
                {
                    string picUri = context.User.GetProperty("avatar_url").GetString();
                    context.Identity.AddClaim(new Claim("picture", picUri));
                    return Task.CompletedTask;
                };
            });
            authenticationBuilder.AddExternalCookie().Configure(options =>
            {
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.HttpOnly = true;
                options.Cookie.Name = "ExternalAuthCookie";
            });
            authenticationBuilder.AddApplicationCookie().Configure(options =>
            {
                options.ExpireTimeSpan = new TimeSpan(0, 60, 0);
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.HttpOnly = true;
                options.Cookie.Name = "AuthenticationCookie";
                options.LoginPath = "/Login";
                options.LogoutPath = "/User/Logout";
                options.SlidingExpiration = false;
            });

            var identityService = services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/ ";
                options.User.RequireUniqueEmail = false;
                options.Stores.MaxLengthForKeys = 128;
            })
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            identityService.AddSignInManager();
            identityService.AddEntityFrameworkStores<ApplicationDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<VideoChatHub>("/videoChatHub");
                endpoints.MapControllers();
                endpoints.MapRazorPages();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
