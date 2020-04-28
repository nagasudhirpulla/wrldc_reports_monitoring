using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WRM.Domain.Entities;
using WRM.Infra.Email;
using WRM.Infra.Identity;
using WRM.Infra.Identity.TokenProviders;
using WRM.App.Security;
using WRM.App.Data;
using WRM.Infra.Reports;
using WRM.Domain.Interfaces.Reports;

namespace WRM.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            if (environment.IsEnvironment("Testing"))
            {
                // Add Identity Infra
                services.AddDbContext<AppDbContext>(options =>
                    options.UseInMemoryDatabase(databaseName: "WrldcReportMonitoring"));
            }
            else
            {
                // Add Identity Persistence Infra 
                services.AddDbContext<AppDbContext>(options =>
                        options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            }

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 2;
                options.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<EmailConfirmationTokenProvider<ApplicationUser>>("emailconfirmation");

            // set email confirmation token lifespan to 3 days
            services.Configure<EmailConfirmationTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromDays(3));

            services.ConfigureApplicationCookie(options =>
            {
                // configure login path for return urls
                // https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-3.1&tabs=visual-studio
                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            });

            // Add Email service
            services.AddTransient<IEmailSender, EmailSender>();
            
            // Add Email service
            services.AddSingleton<IReportsFetchService, ReportsFetchService>();

            // add super admin user details from config as a singleton service
            IdentityInit identityInit = new IdentityInit();
            configuration.Bind("IdentityInit", identityInit);
            services.AddSingleton(identityInit);

            // add email settings from config as a singleton service
            EmailConfiguration emailConfig = new EmailConfiguration();
            configuration.Bind("EmailSettings", emailConfig);
            services.AddSingleton(emailConfig);

            return services;
        }
    }
}
