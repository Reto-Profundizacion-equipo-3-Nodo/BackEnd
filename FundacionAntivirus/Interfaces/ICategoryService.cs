using FundacionAntivirus.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundacionAntivirus.Interfaces
{

    /// Define las operaciones del servicio para la gestión de categorías.
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Obtiene todas las categorías disponibles.
        /// </summary>
        Task<IEnumerable<Category>> GetAllAsync();

        /// <summary>
        /// Obtiene una categoría por su identificador único.
        /// </summary>
        Task<Category?> GetByIdAsync(int id);

        /// <summary>
        /// Crea una nueva categoría en el sistema.
        /// </summary>
        Task<Category> AddAsync(Category category);

        /// <summary>
        /// Actualiza una categoría existente.
        /// </summary>
        Task<Category?> UpdateAsync(Category category);

        /// <summary>
        /// Elimina una categoría por su identificador.
        /// </summary>
        Task<bool> DeleteAsync(int id);
    }
}