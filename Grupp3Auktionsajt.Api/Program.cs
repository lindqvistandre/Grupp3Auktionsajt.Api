using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Grupp3Auktionsajt.Core.Interfaces;
using Grupp3Auktionsajt.Core.Services;
using Grupp3Auktionsajt.Data;
using Grupp3Auktionsajt.Data.Interfaces;
using Grupp3Auktionsajt.Data.Repos;
using Grupp3Auktionsajt.Domain.Models.DTO;
using.Grupp3Auktionsajt.Domain.Models.Entities;
using Grupp3Auktionsajt.Domain.Models.Profiles;
using Microsoft.AspNetCore.SignalR;


var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5265");

builder.Services.AddControllers();
builder.Services.AddSingleton<IDBContext, DBContext>();


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
builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(Uppgift_3_ver_4.Domain.Models.Profiles).Assembly);


var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();