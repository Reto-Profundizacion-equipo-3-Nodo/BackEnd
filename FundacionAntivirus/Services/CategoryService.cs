using FundacionAntivirus.Interfaces;
using FundacionAntivirus.Models;
using FundacionAntivirus.Dtos;

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

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }
        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<Category> AddAsync(CategoryCreateDto categoryCreateDto)
        {
            var category = new Category
            {
                Name = categoryCreateDto.Name,
                Description = categoryCreateDto.Description
            };
            return await _categoryRepository.AddAsync(category);
        }

        public async Task<Category?> UpdateAsync(CategoryUpdateDto categoryUpdateDto)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(categoryUpdateDto.Id);
            if (existingCategory == null) return null;

            existingCategory.Name = categoryUpdateDto.Name;
            existingCategory.Description = categoryUpdateDto.Description;
            return await _categoryRepository.UpdateAsync(categoryUpdateDto);
        }

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