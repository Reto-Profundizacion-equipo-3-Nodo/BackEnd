using FundAntivirus.DTO;
using FundAntivirus.Models;

namespace FundAntivirus.Services
{
    public interface IBootcampInstitutionService
    {
        Task<IEnumerable<BootcampInstitution>> GetAllAsync();
        Task<BootcampInstitution?> GetByIdAsync(int id);
        Task<BootcampInstitution> CreateAsync(BootcampInstitutionDTO dto);
        Task<bool> UpdateAsync(int id, BootcampInstitutionDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
