using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using FundacionAntivirus.Dto;
using FundacionAntivirus.Models;
using FundacionAntivirus.Interfaces;

namespace FundacionAntivirus.Services
{
    public class AuthService: IAuthService
    {

        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Solo genera el token
        public string GenerateJwt(UsersResponseDto dto)
        {
            var claims = new[] {
                new Claim(ClaimTypes.Name, dto.Name),
                new Claim(ClaimTypes.Role, dto.Rol.ToLower()) //Permite que token lleve la informacion del role del usuario y genere el token en minusculas
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}