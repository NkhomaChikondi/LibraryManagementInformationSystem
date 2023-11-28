using LMIS.Api.Core.DataAccess;
using LMIS.Api.Core.Repository;
using LMIS.Api.Core.Repository.IRepository;
using LMIS.Api.Services.Services;
using LMIS.Api.Services.Services.IServices;
using Microsoft.EntityFrameworkCore;

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
