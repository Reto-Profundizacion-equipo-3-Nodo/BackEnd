using FundacionAntivirus.Dtos;

namespace FundacionAntivirus.Interfaces
{
    public interface IUser
    {
        Task<IEnumerable<UserResponseDto>> GetAllAsync();
        Task<UserResponseDto> GetByIdAsync(int id);
        Task CreateAsync(UserRequestDto dto);
        Task UpdateAsync(int id, UserRequestDto dto);
        Task DeleteAsync(int id);
    }
}