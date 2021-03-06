using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WRM.Infra;
using WRM.App;
using FluentValidation.AspNetCore;
using WRM.App.Data;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using MediatR;
using WRM.App.Security.Commands.SeedUsers;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WRM.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public const string ApiAuthSchemes = "Identity.Application," + JwtBearerDefaults.AuthenticationScheme;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructure(Configuration, Environment);
            services.AddApplication();
            services
                .AddControllersWithViews()
                .AddFluentValidation(fv => { fv.RegisterValidatorsFromAssemblyContaining<AppDbContext>(); })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                })
                .AddRazorRuntimeCompilation();


            services.AddRazorPages();

            // share api resource via identity server
            // https://stackoverflow.com/questions/39864550/how-to-get-base-url-without-accessing-a-request
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                // base-address of identityserver
                options.Authority = Configuration["IdentityServer:Authority"];
                options.RequireHttpsMetadata = false;

                // name of the API resource
                options.Audience = "scada_archive";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMediator mediator)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            SeedData(mediator).Wait();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }

        public async Task SeedData(IMediator mediator)
        {
            bool usersSeeded = await mediator.Send(new SeedUsersCommand());
        }
    }
}
