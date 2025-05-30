using FundacionAntivirus.Dto;

namespace FundacionAntivirus.Interfaces
{
    public interface IAuthService
    {
        string GenerateJwt(UsersResponseDto dto);
    }
}