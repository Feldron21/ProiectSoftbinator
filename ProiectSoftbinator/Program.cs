using ProiectSoftbinator.EN;
using ProiectSoftbinator.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using ProiectSoftbinator.Services.RoleServices;
using ProiectSoftbinator.Controllers;
using ProiectSoftbinator.Services.CauseServices;
using ProiectSoftbinator.Services.DonationServices;
using ProiectSoftbinator.Services.UserServices;
using ProiectSoftbinator.Login;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var configurationBuilder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json");

IConfiguration Configuration = configurationBuilder.Build();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = Configuration.GetConnectionString("ConnectionString");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddTransient<ICauseService, CauseService>();
builder.Services.AddTransient<IDonationService, DonationService>();
builder.Services.AddTransient<global::ProiectSoftbinator.Services.UserServices.IUserService, global::ProiectSoftbinator.Services.UserServices.UserService>();

builder.Services.AddTransient<IUserLoginService, UserLoginService>();
builder.Services.AddTransient<ITokenService, TokenService>();

builder.Services
               .AddAuthentication(options =>
               {
                   options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                   options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
               })
               .AddJwtBearer("AuthSchheme", options =>
               {
                   options.RequireHttpsMetadata = true;
                   options.SaveToken = true;
                   options.ClaimsIssuer = builder.Configuration.GetSection("Jwt").GetSection("Issuer").Get<string>();
                   var secret = builder.Configuration.GetSection("Jwt").GetSection("Token").Get<string>();
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       ValidateLifetime = true,
                       RequireExpirationTime = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                       ValidateIssuer = true,
                       ValidateAudience = false,
                       ClockSkew = TimeSpan.Zero
                   };
                   options.Events = new JwtBearerEvents
                   {
                       OnAuthenticationFailed = context =>
                       {
                           if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                           {
                               context.Response.Headers.Add("Token-Expired", "true");
                           }
                           return Task.CompletedTask;
                       }
                   };
               });

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

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
