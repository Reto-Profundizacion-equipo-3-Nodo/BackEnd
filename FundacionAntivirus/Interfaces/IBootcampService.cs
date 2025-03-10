using System.Collections.Generic;
using System.Threading.Tasks;
using FundacionAntivirus.Dtos

namespace Antivirus.Services
{
    public interface IBootcampService
    {
        Task<IEnumerable<BootcampDto>> GetAllAsync();
        Task<BootcampDto> GetByIdAsync(int id);
        Task CreateAsync(BootcampCreateDto Dto);
        Task<BootcampDto> UpdateAsync(int id, BootcampCreateDto bootcampDto);
        Task<bool> DeleteAsync(int id);
    }
}