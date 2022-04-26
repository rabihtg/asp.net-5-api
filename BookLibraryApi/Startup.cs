using BookLibraryClassLibrary.Authentication;
using BookLibraryClassLibrary.Data;
using BookLibraryClassLibrary.DataAccess;
using BookLibraryClassLibrary.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLibraryClassLibrary.Middlewares;

namespace BookLibraryApi
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

            services.AddControllers();

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            //{
            //    opt.SaveToken = true;
            //    var key = Encoding.ASCII.GetBytes(Configuration["JWT:Key"]);
            //    opt.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateAudience = false,
            //        ValidateIssuer = false,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(key)
            //    };
            //});


            services.AddTransient<IJwtManager, JwtManager>();
            services.AddTransient<ISqlDataAccess, SqlDataAccess>();
            services.AddTransient<IBookData, BookData>();
            services.AddTransient<IAuthorData, AuthorData>();
            services.AddTransient<IPublisherData, PublisherData>();
            services.AddTransient<IUserData, UserData>();

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

            //app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
