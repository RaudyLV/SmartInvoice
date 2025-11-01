using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SmartInvoice.Application.Interfaces;
using SmartInvoice.Application.Wrappers;
using SmartInvoice.Infrastructure.Persistence.Data;
using SmartInvoice.Infrastructure.Persistence.Helpers;
using SmartInvoice.Infrastructure.Persistence.Repositories;
using SmartInvoice.Infrastructure.Persistence.Services;

namespace SmartInvoice.Infrastructure
{
    public static class ServiceExtension
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                x => x.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            services.AddScoped<IAuthServices, AuthServices>();
            services.AddScoped<IUserServices, UserService>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<JWTServices>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IUserRoleServices, UserRoleServices>();
            services.AddScoped<IProductServices, ProductServices>();
            services.AddScoped<IClientServices, ClientServices>();
            services.AddScoped<IPaymentServices,PaymentServices>();
            services.AddScoped<IInvoiceServices,InvoiceServices>();


            var key = configuration["JWTSettings:Key"]
                                ?? throw new ArgumentNullException("Key was not found in configuration");

            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                options.DefaultChallengeScheme =
                options.DefaultForbidScheme =
                options.DefaultScheme =
                options.DefaultSignInScheme =
                options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(op =>
            {
                op.RequireHttpsMetadata = false;
                op.SaveToken = false;
                op.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JWTSettings:Issuer"],
                    ValidAudience = configuration["JWTSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    RoleClaimType = ClaimTypes.Role,
                };

                op.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = async context =>
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new Response<string>("Authentication error"));
                        await context.Response.WriteAsync(result);
                    },

                    OnChallenge = async context =>
                    {
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new Response<string>("You are not authenticated."));
                        await context.Response.WriteAsync(result);
                    },

                    OnForbidden = async context =>
                    {
                        context.Response.StatusCode = 403;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new Response<string>("You need authorization for this section."));
                        await context.Response.WriteAsync(result);
                    }
                };
            });   
        }
    }
}