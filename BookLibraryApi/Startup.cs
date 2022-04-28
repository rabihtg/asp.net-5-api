using BookLibraryClassLibrary.Data;
using BookLibraryClassLibrary.DataAccess;
using BookLibraryClassLibrary.JWT;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using BookLibraryClassLibrary.Middlewares;
using Microsoft.AspNetCore.Authorization;
using BookLibraryClassLibrary.Migrations;
using FluentMigrator.Runner;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BookLibraryApi
{
    public class Startup
    {
        public string AllowedOriginsPolicy { get; } = "AllowedOrigins";
        public string MinumumAgePolicy { get; } = "AtLeast18";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
            {
                opt.SaveToken = true;
                var key = Encoding.ASCII.GetBytes(Configuration["JWT:Key"]);
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });


            services.AddAuthorization(opt =>
            {
                opt.AddPolicy(name: MinumumAgePolicy, builder =>
                {
                    builder.AddRequirements(new AgeRequirment(17));
                });
            });



            services.AddFluentMigratorCore()
                .ConfigureRunner(opt =>
                {
                    opt.AddSqlServer()
                    .WithGlobalConnectionString(Configuration.GetConnectionString("Default"))
                    .ScanIn(typeof(Migration_20220428000000).Assembly).For.All();

                }).AddLogging(opt => opt.AddFluentMigratorConsole());


            services.AddSingleton<IAuthorizationHandler, AgeRequirementHandler>();

            services.AddTransient<IJwtManager, JwtManager>();
            services.AddTransient<ISqlDataAccess, SqlDataAccess>();
            services.AddTransient<IBookData, BookData>();
            services.AddTransient<IAuthorData, AuthorData>();
            services.AddTransient<IPublisherData, PublisherData>();
            services.AddTransient<IUserData, UserData>();


            services.AddCors(options =>
            {
                options.AddPolicy(name: AllowedOriginsPolicy, builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookLibraryApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseMiddleware<ExceptionHandlerMiddleware>();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookLibraryApi v1"));
            }

            //app.UseMiddleware<AuthorizationHandlerMiddleWare>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(AllowedOriginsPolicy);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            // Fluent Migrator

            //DataBase.EnsureDataBase(Configuration);
            //app.MigrateDown();
            //app.MigrateUp();

        }
    }
}
