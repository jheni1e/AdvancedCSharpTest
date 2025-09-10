using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RideClub.Endpoints;
using RideClub.Models;
using RideClub.Services.JWT;
using RideClub.Services.Profile;
using RideClub.Services.Rides;
using RideClub.UseCases.CreateProfile;
using RideClub.UseCases.CreateRide;
using RideClub.UseCases.EditRide;
using RideClub.UseCases.GetRide;
using RideClub.UseCases.Login;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RideClubDbContext>(options =>
{
    var sqlConn = "Data Source=localhost; Initial Catalog=RideClub; Trust Server Certificate=true; Integrated Security=true";
    options.UseSqlServer(sqlConn);
});

builder.Services.AddTransient<IProfileService, EFProfileService>();
builder.Services.AddTransient<IRideService, EFRideService>();
builder.Services.AddSingleton<IJWTService, JWTService>();

builder.Services.AddTransient<CreateProfileUseCase>();
builder.Services.AddTransient<CreateRideUseCase>();
builder.Services.AddTransient<EditRideUseCase>();
builder.Services.AddTransient<GetRideUseCase>();
builder.Services.AddTransient<LoginUseCase>();

var jwtSecret = "adbfoahfodsahoifdsjoifjdssoidjfdsfojsfa";
var keyBytes = Encoding.UTF8.GetBytes(jwtSecret);
var key = new SymmetricSecurityKey(keyBytes);

builder.Services.AddSingleton<SecurityKey>(key);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidIssuer = "dtaplace",
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = key,
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.ConfigureAuthEndpoints();
app.ConfigureRideEndpoints();
app.ConfigureUserEndpoints();

app.Run();