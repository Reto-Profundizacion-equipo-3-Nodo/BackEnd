using FundacionAntivirus.Dto;

namespace FundacionAntivirus.Interfaces
{
    public interface IAuthService
    {
        string GenerateJwt(UsersResponseDto dto);
        Task<UsersResponseDto> LoginAsync(UsersRequestDto dto);
        Task<UsersResponseDto> RegisterAsync(UsersRequestDto dto);


    }
}