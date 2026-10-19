using IdentityEx.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityEx.Services
{
    public class TokenService : IToken
    {

        private readonly IConfiguration _config;
        public TokenService(IConfiguration configuration)
        {
            this._config = configuration;
        }
        public string GenerateToken(User user)
        {
            var claims = new List<Claim>(){new
Claim(JwtRegisteredClaimNames.Name,user.FullName),
new
Claim(JwtRegisteredClaimNames.Email,user.Email),
            new Claim("Address",user.Address)
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._config["JWT:screte"]));
          
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                issuer: this._config["JWT:issuer"],
                audience: this._config["JWT:audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
