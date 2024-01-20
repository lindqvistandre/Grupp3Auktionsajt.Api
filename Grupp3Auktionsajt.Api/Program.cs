using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Grupp3Auktionsajt.Data;
using Grupp3Auktionsajt.Data.Interfaces;
using Grupp3Auktionsajt.Data.Repos;
//using Grupp3Auktionsajt.Domain.Models.DTO;
using Grupp3Auktionsajt.Domain.Models.Entities;
using Microsoft.AspNetCore.SignalR;
using AutoMapper;
using Grupp3Auktionsajt.Core.Interfaces;
using Grupp3Auktionsajt.Core.Services;
using Grupp3Auktionsajt.Domain.Models.Profiles;


var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5265");

builder.Services.AddControllers();
builder.Services.AddSingleton<IDBContext, DBContext>();
builder.Services.AddScoped<IAuctionService, AuctionService>();
builder.Services.AddScoped<IAuctionRepo, AuctionRepo>();
builder.Services.AddScoped<IBidService, BidService>();
builder.Services.AddScoped<IBidRepo, BidRepo>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepo, UserRepo>();


// Authentication
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "http://localhost:5265",
        ValidAudience = "http://localhost:5265",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysecretKey12345!#12345555555555555555"))
    };
});

// Automapper
builder.Services.AddAutoMapper(
    typeof(Program).Assembly,
    typeof(GetBidsForAuctionProfile).Assembly,
    typeof(SearchAuctionsProfile).Assembly,
    typeof(GetAuctionProfile).Assembly
    );


var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();