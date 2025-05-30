using FundacionAntivirus.Interfaces;
using FundacionAntivirus.Models;
using FundacionAntivirus.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundacionAntivirus.Services
{
    public class OpportunityInstitutionService : IOpportunityInstitutionService
    {
        private readonly AppDbContext _context;

        public OpportunityInstitutionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OpportunityInstitution>> GetAllAsync()
        {
            return await _context.OpportunityInstitutions.ToListAsync();
        }

        public async Task<OpportunityInstitution?> GetByIdAsync(int id)
        {
            return await _context.OpportunityInstitutions.FindAsync(id);
        }

        public async Task<OpportunityInstitution> AddAsync(OpportunityInstitution institution)
        {
            _context.OpportunityInstitutions.Add(institution);
            await _context.SaveChangesAsync();
            return institution;
        }

        public async Task<OpportunityInstitution?> UpdateAsync(int id, OpportunityInstitution institution)
        {
            var existingInstitution = await _context.OpportunityInstitutions.FindAsync(id);
            if (existingInstitution == null) return null;

            existingInstitution.Name = institution.Name;
            existingInstitution.Description = institution.Description;
            existingInstitution.Address = institution.Address;
            existingInstitution.Phone = institution.Phone;

            await _context.SaveChangesAsync();
            return existingInstitution;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var institution = await _context.OpportunityInstitutions.FindAsync(id);
            if (institution == null) return false;

            _context.OpportunityInstitutions.Remove(institution);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
