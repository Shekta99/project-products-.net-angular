using Microsoft.EntityFrameworkCore;
using ProductsApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using System.Text.Json;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Load the .env file
Env.Load();

builder.Configuration.AddEnvironmentVariables();

//JWT configuration
var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");




// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = jwtIssuer,
         ValidAudience = jwtIssuer,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
     };

     // Customizing the response for unauthorized access
    options.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            // Handle unauthorized access
            context.HandleResponse(); 
            context.Response.StatusCode = StatusCodes.Status401Unauthorized; 
            context.Response.ContentType = "application/json"; 

            var result = JsonSerializer.Serialize(new { error = "Unauthorized access. Please provide a valid token." });
            return context.Response.WriteAsync(result); 
        }
    };
 });
builder.Services.AddDbContext<ProductsContext>(options => 
options.UseMySql($"Server={Environment.GetEnvironmentVariable("DB_SERVER")};" +
                     $"Database={Environment.GetEnvironmentVariable("DB_DATABASE")};" +
                     $"User={Environment.GetEnvironmentVariable("DB_USER")};" +
                     $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD")};" +
                     $"Port={Environment.GetEnvironmentVariable("DB_PORT")}",
        new MySqlServerVersion(new Version(8, 0, 26))));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
}
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
