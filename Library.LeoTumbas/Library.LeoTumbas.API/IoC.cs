using System.Text;
using System.Text.Json.Serialization;
using Library.LeoTumbas.Contracts.Entities;
using Library.LeoTumbas.Contracts.Repositories;
using Library.LeoTumbas.Data.Db.Configurations;
using Library.LeoTumbas.Data.Db.Repositories;
using Library.LeoTumbas.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Library.LeoTumbas.API
{
    public static class IoC
    {
        public static void ConfigureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.AddSecurityDefinition(
                    JwtBearerDefaults.AuthenticationScheme,
                    new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                        Scheme = JwtBearerDefaults.AuthenticationScheme,
                        BearerFormat = "JWT",
                        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                        Description = "JWT Authorization header using the Bearer scheme.",
                    });
                opt.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme,
                            },
                        },
                        new string[]
                        {
                        }
                        },
                    });
            });

            services.AddDbContext<IdentityDbContext<Person, IdentityRole<int>, int>, PeopleDbContext>(
                opt =>
                {
                    opt.UseSqlServer(
                        "Server=localhost\\SQLEXPRESS;Database=LibraryInfinumAPI;Integrated Security=SSPI",
                        opt => opt.MigrationsAssembly("Library.LeoTumbas.Data.Db"));
                });
            services.AddScoped<IPeopleService, DefaultPeopleService>();
            services.AddScoped<IPeopleRepository<Person>, PeopleRepository>();
            services.AddScoped<IRepository<Address>, AddressRepository>();
            services.AddScoped<IUnitOfWork, PeopleUnitOfWork>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IRegistrationService, DefaultRegistrationService>();
            services.AddScoped<IExtendedAuthenticationService, DefaultAuthenticationService>();
            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            services.AddIdentity<Person, IdentityRole<int>>(
                    opt =>
                    {
                        opt.Password.RequiredLength = 10;
                        opt.Password.RequireDigit = true;
                        opt.Password.RequireNonAlphanumeric = true;
                    })
                .AddEntityFrameworkStores<PeopleDbContext>();
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.SaveToken = true;
                opt.RequireHttpsMetadata = false;
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:Audience"],
                    ValidIssuer = configuration["JWT:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
                    ValidateLifetime = true,
                };
            });
        }
    }
}
