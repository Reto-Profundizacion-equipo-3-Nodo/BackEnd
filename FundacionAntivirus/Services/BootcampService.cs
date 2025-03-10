using Antivirus.Data;
using Antivirus.Models;
using Antivirus.Models.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Antivirus.Services
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
            var bootcamps = await _context.bootcamps.ToListAsync();
            return _mapper.Map<IEnumerable<BootcampDto>>(bootcamps);
        }

        public async Task<BootcampDto> GetByIdAsync(int Id)
        {
            var bootcamp = await _context.bootcamps.FindAsync(Id);
            return _mapper.Map<BootcampDto>(bootcamp);
        }

        public async Task CreateAsync(BootcampCreateDto Dto)
        {
            var bootcamp = new Bootcamps
            {
                name = Dto.Name,
                id_cost_id = Dto.IdCostId,
                id_description_id = Dto.DescriptionId,
                id_general_id = Dto.IdGeneralId,
                id_institutionId_id = Dto.institutionId,
                
            };
            _context.bootcamps.Add(bootcampVar);
            await _context.SaveChangesAsync();
        }

        public async Task<BootcampDto> UpdateAsync(int Id, BootcampCreateDto bootcampDto)
        {
            var bootcamp = await _context.bootcamps.FindAsync(Id);
            if (bootcamp == null)
                return null;

            
                name = Dto.Name,
                id_cost_id = Dto.IdCostId,
                id_description_id = Dto.DescriptionId,
                id_general_id = Dto.IdGeneralId,
                id_institutionId_id = Dto.institutionId,

            await _context.SaveChangesAsync();
            return _mapper.Map<BootcampDto>(bootcamp);
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            var bootcamp = await _context.bootcamps.FindAsync(Id);
            if (bootcamp == null) return false;

            _context.bootcamps.Remove(bootcamp);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}