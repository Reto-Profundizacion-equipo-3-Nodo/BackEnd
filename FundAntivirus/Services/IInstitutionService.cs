using FundAntivirus.Models;

namespace FundAntivirus.Services
{
    public interface IInstitutionService
    {
        Task<IEnumerable<Institution>> GetAllInstitutions();
        Task<Institution?> GetInstitutionById(int id);
        Task<Institution> CreateInstitution(CreateInstitutionDto institutionDto);
        Task<Institution?> UpdateInstitution(int id, UpdateInstitutionDto institutionDto);
        Task<bool> DeleteInstitution(int id);
    }
}
