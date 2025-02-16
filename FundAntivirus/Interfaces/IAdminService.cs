using FundAntivirus.Models;
namespace FundAntivirus.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<(bool Success, string Message)> CreateAsync(User User);
        Task<User> UpdateAsync(int id, UpdateUserDto model);
        Task DeleteAsync(int id);

    }
}