using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IdentityAPI.Infrastructure;
using IdentityAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace IdentityAPI.Services
{
    public class IdentityManager : IIdentityManger
    {
        private IdentityDbContext _db;
        private IConfiguration _configuration;
        public IdentityManager(IdentityDbContext db, IConfiguration configuration)
        {
            this._db = db;
            this._configuration = configuration;
        }

        public async Task<dynamic> AddUserAsync(User user)
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            return new
            {
                user.Id,
                user.FirstName,
                user.LastName,
                UserId = user.Email,
                user.ContactNo
            };
        }

        public string ValidateUser(Login login)
        {
            var result = _db.Users.SingleOrDefault(c => c.Email == login.Email && c.Password == login.Password);
            if (result != null)
            {
                string token = GenerateToken(login.Email, login.Password);

                return token;
            }
            return null;
        }

        public string GenerateToken(string email, string password)
        {
            var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, email),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "token");
            if (email == "admin@gmail.com")
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Secret")));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("Jwt:Issuer"),
                audience: _configuration.GetValue<string>("Jwt:Audience"),
                claims: claimsIdentity.Claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
