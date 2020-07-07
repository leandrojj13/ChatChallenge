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
using ChatChallenge.Config;
using ChatChallenge.Core.Models;
using ChatChallenge.Model.Entities.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ChatChallenge.Mock;
using ChatChallenge.Hubs;
using NServiceBus;
using Microsoft.AspNetCore.SignalR;

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
            var tokenSetting = Configuration.GetSection("TokenSetting").Get<TokenSetting>();

            #region CORS
            var appSettings = Configuration.GetSection("AppSettings").Get<AppSettings>();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllPolicy",
                      builder =>
                      {
                          builder
                                 .WithOrigins(appSettings.ClientUrls)
                                 .AllowAnyHeader()
                                 .AllowAnyMethod()
                                 .AllowCredentials();
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
            services.AddSingleton(appSettings);
            services.AddSingleton(tokenSetting);
            #endregion

            #region ContextConfiguration
            string myAppDbContextConnection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ChatChallengeDbContext>(op => op.UseSqlServer(myAppDbContextConnection), ServiceLifetime.Transient);
            #endregion

            #region Adding Settings Sections
            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.Configure<SerilogSettings>(Configuration.GetSection("SerilogSettings"));
            services.Configure<TokenSetting>(Configuration.GetSection("TokenSetting"));
            #endregion

            #region Auth config

            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ChatChallengeDbContext>().AddDefaultTokenProviders();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenSetting.Secret)),
                    ValidIssuer = tokenSetting.Issuer,
                    ValidAudience = tokenSetting.Audience,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddAuthorization();
            #endregion

            #region Adding External Libs
            //Register NServiceBus
            services.AddNServiceBus(Configuration);
            //Register SignalR
            services.AddSignalR();
            //Register Serilog from extension
            services.AddSerilog(Configuration);
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

            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseCors("AllowAllPolicy");
            app.CreateFakeUsers();

            app.ApplicationServices.GetService(typeof(IMessageSession));
            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatRoomHub>("/ChatRoomHub");
            });

            app.UseMvc();
        }
    }
}

