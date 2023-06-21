using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StudyBuddy.Domain.Resources;
using StudyBuddy.Services.Infrastructure;
using System.Reflection;
using System.Text;

namespace StudyBuddy.WebApi.Extensions
{
    public static class AuthExtension
    {
        public static IServiceCollection AddAuth(this IServiceCollection services, JwtSettings settings)
        {
            services.AddAuthentication(cfg => cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = settings.Issuer,
                        ValidAudience = settings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key))
                    };
                });

            return services;
        }

        public static IServiceCollection AddAuthPolicy(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(RoleNames.Administrator,
                    policy => policy.RequireRole(RoleNames.Administrator));

                options.AddPolicy(RoleNames.User,
                    policy => policy.RequireRole(RoleNames.User));

                options.AddPolicy(RoleNames.UserAndAdministrator,
                    policy => policy.RequireRole(RoleNames.User, RoleNames.Administrator));
            });

            return services;
        }

        public static IServiceCollection AddSwGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Study Buddy API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                option.IncludeXmlComments(xmlPath);
            });

            return services;
        }
    }
}
