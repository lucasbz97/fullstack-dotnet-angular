using ManagementUsers.BLL.Interfaces.Services;
using ManagementUsers.BLL.Services;
using ManagementUsers.DAL.Contexts;
using ManagementUsers.DAL.Interfaces;
using ManagementUsers.DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ManagementUsers.BLL.Configuration
{
    public static class ApiModuleDependency
    {
        #region ConfigureServices
        public static void AddConfiguration(this WebApplicationBuilder builder)
        {
            Configuration.ConnectionString = builder.Configuration["ConnectionStrings:DefaultConnection"] ?? string.Empty;
        }
        public static void AddDocumentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(type => type.FullName);
            });
        }

        public static void AddDataContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            });
        }

        public static void AddCrossOrigin(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(
                options => options.AddPolicy(
                    Configuration.CorsPolicyName, policy => policy
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                )
            );
        }

        #endregion

        #region Configure App
        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                });
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IDependentRepository, DependentRepository>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IDependentService, DependentService>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
        #endregion
    }
}
