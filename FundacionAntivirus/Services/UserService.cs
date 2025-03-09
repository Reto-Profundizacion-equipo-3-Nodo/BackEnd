
using AutoMapper;
using FundacionAntivirus.Data;
using FundacionAntivirus.Dtos;
using FundacionAntivirus.Interfaces;
using FundacionAntivirus.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FundacionAntivirus.Services
{
    public class UserService : IUser
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;


        public UserService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateAsync(UserRequestDto dto)
        {
            var entity = _mapper.Map<User>(dto);
            //Hashing the password using Mapper
            entity.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();
            // _mapper.Map<UsersRequestDto>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Users.FindAsync(id);
            if (entity != null)
            {
                _context.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
        {
            var entities = await _context.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserResponseDto>>(entities);
        }

        public async Task<UserResponseDto> GetByIdAsync(int id)
        {
            var entity = await _context.Users.FindAsync(id);
            return _mapper.Map<UserResponseDto>(entity);
        }

        public async Task UpdateAsync(int id, UserRequestDto dto)
        {
            var entity = await _context.Users.FindAsync(id);
            if (entity != null)
            {
                entity.Name = dto.Name;
                entity.Email = dto.Email;
                entity.Password = dto.Password;
                entity.Rol = dto.Rol;
                await _context.SaveChangesAsync();
            }
        }
    }
}