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
using Swashbuckle.AspNetCore.Swagger;
using System;
using AutoMapper;
using ChatChallenge.Services.IoC;
using ChatChallenge.Filters;
using FluentValidation.AspNetCore;
using ChatChallenge.Bl.Validators;

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
            services.AddServicesRegistry();
            services.AddMainRegistry();
            #endregion

            #region ContextConfiguration
            string myAppDbContextConnection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ChatChallengeDbContext>(op => op.UseSqlServer(myAppDbContextConnection), ServiceLifetime.Transient);
            #endregion

            #region Adding External Libs
            //Register AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "ChatChallenge API", Version = "v1" });
            });
            #endregion

            #region Global Api Config
            services.AddMvc(o => { o.Filters.Add<ValidationHttpParametersFilter>();})
               .AddJsonOptions(options => {
                   options.SerializerSettings.ContractResolver =
                       new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
               })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ChatRoomValidator>());


            Dependency.ServiceProvider = services.BuildServiceProvider();
            #endregion
        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChatChallenge API V1");
            });

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

            app.UseHttpsRedirection();

            app.UseCors("AllowAllPolicy");

            app.UseMvc();
        }
    }
}
