using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using FundacionAntivirus.Dtos;
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
        public async Task<UserResponseDto> LoginAsync(UserloginDto dto)
        {
            try
            {

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
                var entity = await _context.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Email == dto.Email);
                if (entity == null || !BCrypt.Net.BCrypt.Verify(dto.Password, entity.Password))
                {
                    throw new UnauthorizedAccessException("Credencaiels invalidas");
                }

                return _mapper.Map<UserResponseDto>(entity);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new UnauthorizedAccessException(ex.Message);
            }catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al intentar loguearse" + ex.Message);
            }
        }
        //Register
        public async Task<UserResponseDto> RegisterAsync(UserRequestDto dto)
        {
            //validar si el rol es vàlido
            var allowedRoles = new string[] { "admin", "user" };
            if (!allowedRoles.Contains(dto.Rol.ToLower()))
            {
                throw new UnauthorizedAccessException("Rol no permitido. Solo se permiten 'user' o 'admin'.");
            }
            //Verificar si el usuario existe en DB
            var existingUser = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == dto.Email);
            if (existingUser != null)
            {
                throw new CustomConflictException("El usuario ya esta registrado");
            }
            var entity = _mapper.Map<User>(dto);
            entity.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserResponseDto>(entity);
        }
        //Genera El Token
        public string GenerateJwt(UserResponseDto dto)
        {
            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, dto.Id.ToString()),
                new Claim(ClaimTypes.Name, dto.Name),
                new Claim(ClaimTypes.Email, dto.Email),
                new Claim(ClaimTypes.Role, dto.Rol.ToLower()) //Permite que token lleve la informacion del role del usuario y genere el token en minusculas
            };

            // Obtener la clave secreta desde la configuración y validar que no sea nula ni vacía

            var jwtKey = _configuration["Jwt:Key"];
            if (string.IsNullOrWhiteSpace(jwtKey))
            {
                throw new InvalidOperationException("La clave secreta 'Jwt:Key' no está configurada en appsettings.json.");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
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