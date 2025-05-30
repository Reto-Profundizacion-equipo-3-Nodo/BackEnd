using FundacionAntivirus.Models;
using FundacionAntivirus.Dto;

namespace FundacionAntivirus.Interfaces
{
    public interface IInstitutionService
    {
        Task<IEnumerable<Institution>> GetAllInstitutions();
        Task<Institution?> GetInstitutionById(int id);
        Task<Institution> CreateInstitution(InstitutionDto institutionDto);
        Task<Institution?> UpdateInstitution(int id, InstitutionDto institutionDto);
        Task<bool> DeleteInstitution(int id);
    }
}