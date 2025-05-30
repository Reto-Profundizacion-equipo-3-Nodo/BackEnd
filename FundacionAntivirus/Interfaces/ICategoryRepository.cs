using FundacionAntivirus.Models;
using FundacionAntivirus.Dtos;

namespace FundacionAntivirus.Interfaces;

/// <summary>
/// Define las operaciones CRUD para la gestión de categorías en el repositorio.
/// </summary>
public interface ICategoryRepository
{
    /// <summary>
    /// Obtiene todas las categorías disponibles en el sistema.
    /// </summary>
    /// <returns>Una lista de categorías.</returns>
    Task<IEnumerable<Category>> GetAllAsync();

    /// <summary>
    /// Obtiene una categoría específica según su identificador.
    /// </summary>
    /// <param name="id">El ID de la categoría.</param>
    /// <returns>La categoría encontrada o null si no existe.</returns>
    Task<Category?> GetByIdAsync(int id);

    /// <summary>
    /// Agrega una nueva categoría al sistema.
    /// </summary>
    /// <param name="category">Los datos de la categoría a agregar.</param>
    /// <returns>La categoría creada con su ID asignado.</returns>
    Task<Category> AddAsync(Category category);

    /// <summary>
    /// Actualiza una categoría existente en el sistema.
    /// </summary>
    /// <param name="category">La categoría con los datos actualizados.</param>
    /// <returns>La categoría actualizada o null si no se encontró.</returns>
    Task<Category?> UpdateAsync(CategoryUpdateDto categoryUpdateDto);

    /// <summary>
    /// Elimina una categoría del sistema según su identificador.
    /// </summary>
    /// <param name="id">El ID de la categoría a eliminar.</param>
    /// <returns>True si la eliminación fue exitosa, false si no se encontró la categoría.</returns>
    Task<bool> DeleteAsync(int id);
}
