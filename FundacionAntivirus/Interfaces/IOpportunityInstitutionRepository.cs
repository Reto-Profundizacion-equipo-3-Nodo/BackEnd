using FundacionAntivirus.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundacionAntivirus.Interfaces
{
    public interface IOpportunityInstitutionRepository
    {
        Task<IEnumerable<OpportunityInstitution>> GetAllAsync();
        Task<OpportunityInstitution?> GetByIdAsync(int id);
        Task<OpportunityInstitution> AddAsync(OpportunityInstitution institution);
        Task<OpportunityInstitution?> UpdateAsync(int id, OpportunityInstitution institution);
        Task<bool> DeleteAsync(int id);
    }
}
