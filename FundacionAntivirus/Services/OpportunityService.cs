using FundacionAntivirus.Interfaces;
using FundacionAntivirus.Models;

namespace FundacionAntivirus.Services
{
    /// <summary>
    /// Implementación del servicio para la gestión de oportunidades.
    /// </summary>
    public class OpportunityService : IOpportunityService
    {
        private readonly IOpportunityRepository _opportunityRepository;
        private readonly ILogger<OpportunityService> _logger;

        public OpportunityService(IOpportunityRepository opportunityRepository, ILogger<OpportunityService> logger)
        {
            _opportunityRepository = opportunityRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Opportunity>> GetAllOpportunitiesAsync()
        {
            return await _opportunityRepository.GetAllAsync();
        }

        public async Task<Opportunity?> GetOpportunityByIdAsync(int id)
        {
            return await _opportunityRepository.GetByIdAsync(id);
        }

        public async Task<Opportunity> CreateOpportunityAsync(Opportunity opportunity)
        {
            return await _opportunityRepository.AddAsync(opportunity);
        }

        public async Task<Opportunity?> UpdateOpportunityAsync(int id, Opportunity opportunity)
        {
            return await _opportunityRepository.UpdateAsync(id, opportunity);
        }

        public async Task<bool> DeleteOpportunityAsync(int id)
        {
            return await _opportunityRepository.DeleteAsync(id);
        }
    }
}