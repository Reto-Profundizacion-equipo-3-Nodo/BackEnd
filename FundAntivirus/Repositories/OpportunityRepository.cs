namespace FundAntivirus.Repositories
{
    using FundAntivirus.DTOs.Opportunity;
    using FundAntivirus.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Implementación del repositorio de oportunidades.
    /// </summary>
    public class OpportunityRepository : IOpportunityRepository
    {
        private readonly List<OpportunityResponseDTO> _opportunities = new();

        /// <inheritdoc/>
        public async Task<IEnumerable<OpportunityResponseDTO>> GetAllAsync()
        {
            return await Task.FromResult(_opportunities);
        }

        /// <inheritdoc/>
        public async Task<OpportunityResponseDTO?> GetByIdAsync(int id)
        {
            var opportunity = _opportunities.FirstOrDefault(o => o.Id == id);
            return await Task.FromResult(opportunity);
        }

        /// <inheritdoc/>
        public async Task<OpportunityResponseDTO> CreateAsync(CreateOpportunityDTO dto)
        {
            var newOpportunity = new OpportunityResponseDTO
            {
                Id = _opportunities.Count + 1, // Simulación de autoincremento
                Name = dto.Name,
                Description = dto.Description,
                CategoryId = dto.CategoryId,
                UserId = dto.UserId,
                Status = dto.Status
            };

            _opportunities.Add(newOpportunity);
            return await Task.FromResult(newOpportunity);
        }

        /// <inheritdoc/>
        public async Task<OpportunityResponseDTO?> UpdateAsync(int id, UpdateOpportunityDTO dto)
        {
            var opportunity = _opportunities.FirstOrDefault(o => o.Id == id);
            if (opportunity == null) return null;

            opportunity.Name = dto.Name ?? opportunity.Name;
            opportunity.Description = dto.Description ?? opportunity.Description;
            opportunity.Status = dto.Status;


            return await Task.FromResult(opportunity);
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(int id)
        {
            var opportunity = _opportunities.FirstOrDefault(o => o.Id == id);
            if (opportunity == null) return false;

            _opportunities.Remove(opportunity);
            return await Task.FromResult(true);
        }
    }
}
