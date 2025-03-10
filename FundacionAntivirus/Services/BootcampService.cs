using FundacionAntivirus.Data;
using FundacionAntivirus.Models;
using FundacionAntivirus.Dtos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace FundacionAntivirus.Services
{
    public class BootcampService : IBootcampService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BootcampService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BootcampDto>> GetAllAsync()
        {
            var bootcamps = await _context.Bootcamps.ToListAsync();
            return _mapper.Map<IEnumerable<BootcampDto>>(bootcamps);
        }

        public async Task<BootcampDto> GetByIdAsync(int Id)
        {
            var bootcamp = await _context.Bootcamps.FindAsync(Id);
            return _mapper.Map<BootcampDto>(bootcamp);
        }

        public async Task CreateAsync(BootcampCreateDto Dto)
        {
            var bootcamp = new Bootcamp
            {
                Name = Dto.Name,
                Description = Dto.Description,
                Information = Dto.Information,
                Costs = Dto.Costs,
                InstitutionId = Dto.InstitutionId,
                
            };
            _context.Bootcamps.Add(bootcamp);
            await _context.SaveChangesAsync();
        }

        public async Task<BootcampDto> UpdateAsync(int Id, BootcampCreateDto bootcampDto)
        {
            var bootcamp = await _context.Bootcamps.FindAsync(Id);
            if (bootcamp != null)
                return null;
                bootcamp.Name = bootcampDto.Name;
                bootcamp.Description = bootcampDto.Description;
                bootcamp.Information = bootcampDto.Information;
                bootcamp.Costs = bootcampDto.Costs;
                bootcamp.InstitutionId = bootcampDto.InstitutionId;

            await _context.SaveChangesAsync();
            return _mapper.Map<BootcampDto>(bootcamp);
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            var bootcamp = await _context.Bootcamps.FindAsync(Id);
            if (bootcamp == null) return false;

            _context.Bootcamps.Remove(bootcamp);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}