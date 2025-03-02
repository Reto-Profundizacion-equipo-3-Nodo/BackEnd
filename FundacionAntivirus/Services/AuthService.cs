using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using FundacionAntivirus.Dto;
using FundacionAntivirus.Interfaces;
using FundacionAntivirus.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FundacionAntivirus.Models;

namespace FundacionAntivirus.Services
{
    public class AuthService : IAuthService
    {

        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AuthService(IConfiguration configuration, AppDbContext context, IMapper mapper)
        {
            _configuration = configuration;
            _context = context;
            _mapper = mapper;
        }
        // Login
        public async Task<UsersResponseDto> LoginAsync(UsersRequestDto dto)
        {
            //Validar si ingreso un rol permitido
            var allowedRoles = new string[] { "admin", "user" };
            if (!allowedRoles.Contains(dto.Rol.ToLower()))
            {
                throw new UnauthorizedAccessException("Rol no permitido. Solo se permiten 'user' o 'admin'.");
            }
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var entity = await _context.users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.email == dto.Email);
            if (entity == null || !BCrypt.Net.BCrypt.Verify(dto.Password, entity.password))
            {
                throw new UnauthorizedAccessException("Credencaiels invalidas");
            }

            return _mapper.Map<UsersResponseDto>(entity);
        }
        //Register
        public async Task<UsersResponseDto> RegisterAsync(UsersRequestDto dto)
        {
            //validar si el rol es vàlido
            var allowedRoles = new string[] { "admin", "user" };
            if (!allowedRoles.Contains(dto.Rol.ToLower()))
            {
                throw new UnauthorizedAccessException("Rol no permitido. Solo se permiten 'user' o 'admin'.");
            }
            //Verificar si el usuario existe en DB
            var existingUser = await _context.users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.email == dto.Email);
            if (existingUser != null)
            {
                throw new UnauthorizedAccessException("El usuario ya esta registrado");
            }
            var entity = _mapper.Map<users>(dto);
            entity.password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            _context.users.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<UsersResponseDto>(entity);
        }
        //Genera El Token
        public string GenerateJwt(UsersResponseDto dto)
        {
            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, dto.Id.ToString()),
                new Claim(ClaimTypes.Name, dto.Name),
                new Claim(ClaimTypes.Email, dto.Email),
                new Claim(ClaimTypes.Role, dto.Rol.ToLower()) //Permite que token lleve la informacion del role del usuario y genere el token en minusculas
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}