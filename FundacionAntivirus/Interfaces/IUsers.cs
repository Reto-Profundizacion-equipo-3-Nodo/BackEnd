using FundacionAntivirus.Dto;

namespace FundacionAntivirus.Interfaces
{
    public interface IUsers
    {
        Task<IEnumerable<UsersResponseDto>> GetAllAsync();
        Task<UsersResponseDto> GetByIdAsync(int id);
        Task CreateAsync(UsersRequestDto dto);
        Task UpdateAsync(int id, UsersRequestDto dto);
        Task DeleteAsync(int id);
    }
}