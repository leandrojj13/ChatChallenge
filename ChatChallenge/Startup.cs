using ChatChallenge.Bl.IoC;
using ChatChallenge.Core.IoC;
using ChatChallenge.Core.IoC.Config;
using ChatChallenge.IoC;
using ChatChallenge.Model.Contexts.ChatChallenge;
using ChatChallenge.Model.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatChallenge
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

            #region CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllPolicy",
                      builder =>
                      {
                          builder
                                 .WithOrigins("http://localhost:8080")
                                 .AllowAnyHeader()
                                 .AllowAnyMethod()
                                 .AllowCredentials()
                                 ;
                          builder.SetIsOriginAllowed(x => true);

                      });
            });
            #endregion

            #region IoC Registry
            services.AddCoreRegistry();
            services.AddModelRegistry();
            services.AddBlRegistry();
            services.AddMainRegistry();
            #endregion

            #region ContextConfiguration
            string myAppDbContextConnection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ChatChallengeDbContext>(op => op.UseSqlServer(myAppDbContextConnection), ServiceLifetime.Transient);
            #endregion

            #region Global Api Config
            services.AddMvc()
               .AddJsonOptions(options => {
                   options.SerializerSettings.ContractResolver =
                       new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
               })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            Dependency.ServiceProvider = services.BuildServiceProvider();
            #endregion
        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAllPolicy");

            app.UseMvc();
        }
    }
}
