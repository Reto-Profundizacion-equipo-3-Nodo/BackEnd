using FundacionAntivirus.Dtos;

namespace FundacionAntivirus.Interfaces
{
    public interface IUser
    {
        Task<IEnumerable<UserResponseDto>> GetAllAsync();
        Task<UserResponseDto> GetByIdAsync(int id);
        Task CreateUserAsync(UserRequestDto dto);
        Task UpdateUserAsync(int id, UserRequestDto dto);
        Task DeleteUserAsync(int id);
    }
}