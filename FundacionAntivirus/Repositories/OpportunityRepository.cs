using FundacionAntivirus.Interfaces;
using FundacionAntivirus.Models;
using FundacionAntivirus.Data;
using Microsoft.EntityFrameworkCore;

namespace FundacionAntivirus.Repositories
{
    /// <summary>
    /// Repositorio para la gestión de oportunidades en la base de datos.
    /// </summary>
    public class OpportunityRepository : IOpportunityRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructor del repositorio de oportunidades.
        /// </summary>
        public OpportunityRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Opportunity>> GetAllAsync()
        {
            return await _context.Opportunities.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Opportunity?> GetByIdAsync(int id)
        {
            return await _context.Opportunities.FindAsync(id);
        }

        /// <inheritdoc/>
        public async Task<Opportunity> AddAsync(Opportunity opportunity)
        {
            _context.Opportunities.Add(opportunity);
            await _context.SaveChangesAsync();
            return opportunity;
        }

        /// <inheritdoc/>
        public async Task<Opportunity?> UpdateAsync(int id, Opportunity opportunity)
        {
            var existingOpportunity = await _context.Opportunities.FindAsync(id);
            if (existingOpportunity == null) return null;

            _context.Entry(existingOpportunity).CurrentValues.SetValues(opportunity);
            await _context.SaveChangesAsync();
            return existingOpportunity;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(int id)
        {
            var opportunity = await _context.Opportunities.FindAsync(id);
            if (opportunity == null) return false;

            _context.Opportunities.Remove(opportunity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}