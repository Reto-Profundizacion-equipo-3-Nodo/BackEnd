using FundAntivirus.DTO;
using FundAntivirus.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundAntivirus.Services
{
    public class BootcampInstitutionService : IBootcampInstitutionService
    {
        private readonly ApplicationDbContext _context;

        public BootcampInstitutionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BootcampInstitutionDTO>> GetAllAsync()
        {
            return await _context.BootcampInstitutions
                .Select(bi => new BootcampInstitutionDTO
                {
                    Id = bi.Id,
                    InstitutionId = bi.InstitutionId,
                    BootcampId = bi.BootcampId,
                    TopicId = bi.TopicId
                }).ToListAsync();
        }

        public async Task<BootcampInstitutionDTO?> GetByIdAsync(int id)
        {
            var bi = await _context.BootcampInstitutions.FindAsync(id);
            if (bi == null) return null;

            return new BootcampInstitutionDTO
            {
                Id = bi.Id,
                InstitutionId = bi.InstitutionId,
                BootcampId = bi.BootcampId,
                TopicId = bi.TopicId
            };
        }

        public async Task<BootcampInstitutionDTO> CreateAsync(BootcampInstitutionDTO dto)
        {
            var entity = new BootcampInstitution
            {
                InstitutionId = dto.InstitutionId,
                BootcampId = dto.BootcampId,
                TopicId = dto.TopicId
            };

            _context.BootcampInstitutions.Add(entity);
            await _context.SaveChangesAsync();

            dto.Id = entity.Id;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, BootcampInstitutionDTO dto)
        {
            var bi = await _context.BootcampInstitutions.FindAsync(id);
            if (bi == null) return false;

            bi.InstitutionId = dto.InstitutionId;
            bi.BootcampId = dto.BootcampId;
            bi.TopicId = dto.TopicId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var bi = await _context.BootcampInstitutions.FindAsync(id);
            if (bi == null) return false;

            _context.BootcampInstitutions.Remove(bi);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
