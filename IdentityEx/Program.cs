using AutoMapper;
using IdentityEx.AutMapperConfig;
using IdentityEx.Models;
using IdentityEx.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<CompanyContext>();
builder.Services.AddControllers();
builder.Services.AddDbContextPool<CompanyContext>(opt => opt.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("scon")));

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

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
c.AddSecurityDefinition("Bearer", new
OpenApiSecurityScheme
{
    Name = "Authorization",
    Type = SecuritySchemeType.Http,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "Enter 'Bearer' [space] and then your valid JWT token.\n\nExample:`Bearer abc123def456`"
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
        Array.Empty<string>()
    }
});
});

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
