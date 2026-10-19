using IdentityEx.Models;
using IdentityEx.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<CompanyContext>();
builder.Services.AddControllers();
builder.Services.AddDbContextPool<CompanyContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("scon")));

var issuer = builder.Configuration["JWT:issuer"];
var audience = builder.Configuration["JWT:audience"];
var screte = builder.Configuration["JWT:screte"];
var bytes = Encoding.UTF8.GetBytes(screte);
var key = new SymmetricSecurityKey(bytes);

builder.Services.AddAuthentication(option =>
{
option.DefaultAuthenticateScheme =
option.DefaultChallengeScheme =
option.DefaultForbidScheme = 
option.DefaultScheme =
option.DefaultSignInScheme =
option.DefaultSignOutScheme =
JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt => {
    opt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer=true,
        ValidateAudience=true,
        ValidateLifetime=true,
        ValidateIssuerSigningKey=true,
        ValidIssuer=issuer,
        ValidAudience=audience,
        IssuerSigningKey=key
    };

});
builder.Services.AddScoped<IToken, TokenService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
