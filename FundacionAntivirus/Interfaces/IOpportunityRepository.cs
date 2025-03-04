using FundacionAntivirus.Models;

namespace FundacionAntivirus.Repositories.Interfaces;

/// <summary>
/// Interface para el repositorio de oportunidades.
/// </summary>
public interface IOpportunityRepository
{
    Task<IEnumerable<Opportunity>> GetAllAsync();
    Task<Opportunity?> GetByIdAsync(int id);
    Task AddAsync(Opportunity opportunity);
    Task UpdateAsync(Opportunity opportunity);
    Task DeleteAsync(int id);
}
