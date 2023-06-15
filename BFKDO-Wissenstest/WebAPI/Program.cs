
using Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Reflection;
using System.Text;
using WebAPI.Identity;

namespace WebAPI
{
    /// <summary>
    ///     Programm Class of Web API.
    /// </summary>
    public class Program
    {
        /// <summary>
        ///     Startpoint of Program.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            //var CORS = "corspolicy";
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
#if DEBUG
                        policy./*WithOrigins("https://localhost:7022", "https://localhost:7227")*/AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
#else
                        policy.WithOrigins("https://bfkdo-adminapp.azurewebsites.net", "https://bfkdo-benutzerapp.azurewebsites.net").AllowAnyHeader().AllowAnyMethod();
#endif
                    });
            });

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            builder.Services.AddAuthentication(authentication =>
            {
                authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                authentication.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(bearer =>
            {
                bearer.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = builder.Configuration["JWTSettings:Issuer"],
                    ValidAudience = builder.Configuration["JWTSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTSettings:Key"]!)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(Identities.AdminPolicyName, p => p.RequireClaim(Identities.AdminClaimName, "true"));
                options.AddPolicy(Identities.EvaluatorPolicyName, p => p.RequireClaim(Identities.EvaluatorClaimName, "true"));
                options.AddPolicy(Identities.ParticipantPolicyName, p => p.RequireClaim(Identities.ParticipantClaimName, "true"));
            });

            // Add Database to Container.
            builder.Services.AddDbContext<BfkdoDbContext>(config =>
            {
                config.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}