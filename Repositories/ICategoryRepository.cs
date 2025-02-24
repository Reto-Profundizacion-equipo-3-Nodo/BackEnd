using System.Collections.Generic;
using System.Threading.Tasks;
using FundAntivirus.Models;

namespace FundAntivirus.Repositories
{
    // Interfaz que define las operaciones para manejar categorías.
    public interface ICategoryRepository
    {
        Task<bool> Exists(string name); //Verifica si una categoría ya existe.
        Task<IEnumerable<Category>> GetAllCategories(); // Obtener todas las categorías.
        Task<Category?> GetCategoryById(int id); // Obtener una categoría por ID.
        Task<Category> CreateCategory(Category category); // Crear una nueva categoría.
        Task<Category> UpdateCategory(Category category); // Actualizar una categoría existente.
        Task<bool> DeleteCategory(int id); // Eliminar una categoría por ID.
    }
}
