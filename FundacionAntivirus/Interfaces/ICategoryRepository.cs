using FundacionAntivirus.Models;

namespace FundacionAntivirus.Repositories.Interfaces;

/// <summary>
/// Interface para el repositorio de categorías.
/// </summary>
public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
    Task AddAsync (Category category);
    Task UpdateAsync(Category category);
    Task DeleteAsync (int id);
}