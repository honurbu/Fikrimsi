using Fikrimsi.Core.Configuration;
using Fikrimsi.Core.Repositories;
using Fikrimsi.Core.Services;
using Fikrimsi.Core.UnitOfWork;
using Fikrimsi.Repository.Repositories;
using Fikrimsi.Repository.UnitOfWorks;
using Fikrimsi.Service.Mapping;
using Fikrimsi.Service.Services;
using Fikrimsi.SharedLibrary.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Configuration and Environment
var configuration = builder.Configuration;
var environment = builder.Environment;

// Add services to the container.

// DI Registers





builder.Services.Configure<CustomTokenOptions>(configuration.GetSection("TokenOption"));
builder.Services.Configure<List<Client>>(configuration.GetSection("Clients"));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Move UseAuthentication before UseAuthorization
app.UseAuthorization();
app.MapControllers();

app.Run();
