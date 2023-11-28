using LMIS.Api.Core.DataAccess;
using LMIS.Api.Core.Model;
using LMIS.Api.Core.Repository;
using LMIS.Api.Core.Repository.IRepository;
using LMIS.Api.Services.Services;
using LMIS.Api.Services.Services.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var provider = builder.Configuration["ServerSettings:ServerName"];
string postgreSQLConnection = builder.Configuration.GetConnectionString("PostgreSQLConnection");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailService, EmailService>();



// configuring the token and making sure that the user has the correct token all the time
var key = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("TokenString:TokenKey"));

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x =>
    {
        x.Events = new JwtBearerEvents
        {
            OnTokenValidated = context =>
            {
                // Assuming the token contains user ID in the claim
                var userIdClaim = context.Principal.FindFirst(ClaimTypes.NameIdentifier);

                if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
                {
                    context.Fail("Unauthorized: User ID not found in token.");
                    return Task.CompletedTask;
                }

                // Get user details based on the user ID 
                var userId = userIdClaim.Value;
                var userRepository = context.HttpContext.RequestServices.GetService<IUnitOfWork>(); 
                var user = userRepository.User.GetByIdAsync(Convert.ToInt32( userId)); 

                if (user == null)
                {
                    context.Fail("Unauthorized: User not found.");
                    return Task.CompletedTask;
                }               

                return Task.CompletedTask;
            }
        };
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateAudience = false
        };

    });
builder.Services.AddDbContext<ApplicationDbContext>(
options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("conn"));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
