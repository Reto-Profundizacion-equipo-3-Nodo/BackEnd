
using AutoMapper;
using FundacionAntivirus.Data;
using FundacionAntivirus.Dto;
using FundacionAntivirus.Interface;
using FundacionAntivirus.Models;
using Microsoft.EntityFrameworkCore;

namespace FundacionAntivirus.Services
{
    public class UsersService : IUsers
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;


        public UsersService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateAsync(UsersRequestDto dto)
        {
            var entity = _mapper.Map<users>(dto);
            _context.users.Add(entity);
            await _context.SaveChangesAsync();
            _mapper.Map<UsersRequestDto>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.users.FindAsync(id);
            if (entity != null)
            {
                _context.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<UsersResponseDto>> GetAllAsync()
        {
            var entities = await _context.users.ToListAsync();
            return _mapper.Map<IEnumerable<UsersResponseDto>>(entities);
        }

        public async Task<UsersResponseDto> GetByIdAsync(int id)
        {
            var entity = await _context.users.FindAsync(id);
            return _mapper.Map<UsersResponseDto>(entity);
        }

        public async Task UpdateAsync(int id, UsersRequestDto dto)
        {
            var entity = await _context.users.FindAsync(id);
            if (entity != null)
            {
                entity.name = dto.Name;
                entity.email = dto.Email;
                entity.password = dto.Password;
                entity.rol = dto.Rol;
                await _context.SaveChangesAsync();
            }
        }
    }
}