using FundacionAntivirus.Dtos;

namespace FundacionAntivirus.Interfaces
{
    public interface IAuthService
    {
        string GenerateJwt(UserResponseDto dto);
        Task<UserResponseDto> LoginAsync(UserRequestDto dto);
        Task<UserResponseDto> RegisterAsync(UserRequestDto dto);


    }
}