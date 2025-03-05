using FundacionAntivirus.Data;
using FundacionAntivirus.Models;
using FundacionAntivirus.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FundacionAntivirus.Repositories;

/// <summary>
/// Repositorio para la gestión de oportunidades.
/// </summary>
public class OpportunityRepository : IOpportunityRepository
{
    private readonly AppDbContext _context;

    public OpportunityRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Opportunity>> GetAllAsync()
    {
        return await _context.Opportunities.ToListAsync();
    }

    public async Task<Opportunity?> GetByIdAsync(int id)
    {
        return await _context.Opportunities.FindAsync(id);
    }

    public async Task AddAsync(Opportunity opportunity)
    {
        await _context.Opportunities.AddAsync(opportunity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Opportunity opportunity)
    {
        _context.Opportunities.Update(opportunity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var opportunity = await _context.Opportunities.FindAsync(id);
        if (opportunity != null)
        {
            _context.Opportunities.Remove(opportunity);
            await _context.SaveChangesAsync();
        }
    }
}