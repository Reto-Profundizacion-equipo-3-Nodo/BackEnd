using FundacionAntivirus.Interfaces;
using FundacionAntivirus.Models;
using FundacionAntivirus.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundacionAntivirus.Services
{
    /// <summary>
    /// Implementación del servicio para la gestión de categorías.
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(ICategoryRepository categoryRepository, ILogger<CategoryService> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todas las categorías.
        /// </summary>
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            try
            {
                return await _categoryRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las categorías");
                return new List<Category>();
            }
        }

        /// <summary>
        /// Obtiene una categoría por su ID.
        /// </summary>
        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("El ID proporcionado no es válido: {Id}", id);
                return null;
            }

            try
            {
                return await _categoryRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la categoría con ID {Id}", id);
                return null;
            }
        }

        /// <summary>
        /// Crea una nueva categoría.
        /// </summary>
        public async Task<Category?> CreateCategoryAsync(Category category)
        {
            if (category == null)
            {
                _logger.LogWarning("Intento de crear una categoría con un valor nulo.");
                return null;
            }

            try
            {
                return await _categoryRepository.AddAsync(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear una nueva categoría");
                return null;
            }
        }

        /// <summary>
        /// Actualiza una categoría existente.
        /// </summary>
        public async Task<Category?> UpdateCategoryAsync(int id, Category category)
        {
            if (id <= 0 || category == null)
            {
                _logger.LogWarning("Datos de actualización no válidos. ID: {Id}", id);
                return null;
            }

            try
            {
                return await _categoryRepository.UpdateAsync(id, category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la categoría con ID {Id}", id);
                return null;
            }
        }

        /// <summary>
        /// Elimina una categoría por su ID.
        /// </summary>
        public async Task<bool> DeleteCategoryAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Intento de eliminar una categoría con un ID inválido: {Id}", id);
                return false;
            }

            try
            {
                return await _categoryRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la categoría con ID {Id}", id);
                return false;
            }
        }
    }
}