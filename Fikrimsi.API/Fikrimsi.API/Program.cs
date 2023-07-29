using Fikrimsi.Core.Configuration;
using Fikrimsi.Core.Entities;
using Fikrimsi.Core.Repositories;
using Fikrimsi.Core.Services;
using Fikrimsi.Core.UnitOfWork;
using Fikrimsi.Repository.Context;
using Fikrimsi.Repository.Repositories;
using Fikrimsi.Repository.UnitOfWorks;
using Fikrimsi.Service.Mapping;
using Fikrimsi.Service.Services;
using Fikrimsi.SharedLibrary.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configuration and Environment
var configuration = builder.Configuration;
var environment = builder.Environment;

// Add services to the container.

// DI Registers

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICommentService,CommentService>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(ICommentRepository), typeof(CommentRepository));
builder.Services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAutoMapper(typeof(MapProfile));

// Database Connection and DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("SqlServerDbContext"), sqlOptions =>
    {
        sqlOptions.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});

// User Identity and User Roles (default by IdentityRole) but we could make a new one for Role like UserRole class, etc.
builder.Services.AddIdentity<UserApp, IdentityRole>(Opt =>
{
    Opt.User.RequireUniqueEmail = true;
    Opt.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();



builder.Services.Configure<CustomTokenOptions>(configuration.GetSection("TokenOption"));
builder.Services.Configure<List<Client>>(configuration.GetSection("Clients"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
{
    var tokenOptions = configuration.GetSection("TokenOption").Get<CustomTokenOptions>();

    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience[0],
        IssuerSigningKey = SignService.GetSymmetricSecurtyKey(tokenOptions.SecurityKey),

        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(options =>
//{
//    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
//    {
//        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
//        In = ParameterLocation.Header,
//        Name = "Authorization",
//        Type = SecuritySchemeType.ApiKey
//    });
//});


builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                new string[] {}
            }
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // Move UseAuthentication before UseAuthorization
app.UseAuthorization();
app.MapControllers();

app.Run();
