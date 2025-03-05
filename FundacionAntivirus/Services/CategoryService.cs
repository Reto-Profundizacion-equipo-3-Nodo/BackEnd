using FundacionAntivirus.Interfaces;
using FundacionAntivirus.Models;

namespace FundacionAntivirus.Services
{
    /// <summary>
    /// Implementación del servicio para la gestión de categorías.
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryService> _logger;

        /// <summary>
        /// Constructor del servicio de categorías.
        /// </summary>
        /// <param name="categoryRepository">Repositorio de categorías.</param>
        /// <param name="logger">Logger para registrar eventos e información.</param>
        public CategoryService(ICategoryRepository categoryRepository, ILogger<CategoryService> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todas las categorías disponibles.
        /// </summary>
        /// <returns>Lista de categorías.</returns>
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        /// <summary>
        /// Obtiene una categoría específica por su ID.
        /// </summary>
        /// <param name="id">El ID de la categoría a buscar.</param>
        /// <returns>La categoría encontrada o null si no existe.</returns>
        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Agrega una nueva categoría.
        /// </summary>
        /// <param name="category">Objeto de la categoría a agregar.</param>
        /// <returns>La categoría creada.</returns>
        public async Task<Category> AddAsync(Category category)
        {
            return await _categoryRepository.AddAsync(category);
        }

        /// <summary>
        /// Actualiza una categoría existente.
        /// </summary>
        /// <param name="category">Objeto con la información actualizada de la categoría.</param>
        /// <returns>La categoría actualizada o null si no se encontró.</returns>
        public async Task<Category?> UpdateAsync(Category category)
        {
            return await _categoryRepository.UpdateAsync(category);
        }

        /// <summary>
        /// Elimina una categoría por su ID.
        /// </summary>
        /// <param name="id">El ID de la categoría a eliminar.</param>
        /// <returns>True si la categoría fue eliminada, false si no se encontró.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(id);
            if (existingCategory == null)
            {
                _logger.LogWarning($"Intento de eliminar una categoría inexistente con ID {id}");
                return false;
            }

            return await _categoryRepository.DeleteAsync(id);
        }
    }
}