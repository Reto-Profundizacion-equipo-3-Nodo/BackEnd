using FundacionAntivirus.Dtos;

namespace FundacionAntivirus.Interfaces
{
    public interface IAuthService
    {
        string GenerateJwt(UserResponseDto dto);
        Task<UserResponseDto> LoginAsync(UserloginDto dto);
        Task<UserResponseDto> RegisterAsync(UserRequestDto dto);


    }
}