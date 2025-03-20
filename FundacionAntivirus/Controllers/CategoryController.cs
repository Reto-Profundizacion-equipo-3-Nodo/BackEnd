using FundacionAntivirus.Interfaces;
using FundacionAntivirus.Models;
using FundacionAntivirus.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FundacionAntivirus.Controllers
{
    /// <summary>
    /// Controlador para gestionar las categorías de la aplicación.
    /// Proporciona endpoints para obtener, crear, actualizar y eliminar categorías.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        /// <summary>
        /// Constructor del controlador de categorías.
        /// </summary>
        /// <param name="categoryService">Servicio de categoría.</param>
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Obtiene todas las categorías disponibles.
        /// </summary>
        /// <returns>Una lista de categorías.</returns>
        [HttpGet]
        [Authorize(Roles = "admin,user")] // Tanto admin como user pueden ver todas las categorías
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await _categoryService.GetAllAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene una categoría específica por su ID.
        /// </summary>
        /// <param name="id">ID de la categoría.</param>
        /// <returns>La categoría encontrada o un mensaje de error si no existe.</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "admin,user")] // Tanto admin como user pueden ver una categoría específica
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                var category = await _categoryService.GetByIdAsync(id);
                if (category == null)
                    return NotFound($"Categoría con ID {id} no encontrada.");

                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Crea una nueva categoría.
        /// </summary>
        /// <param name="categoryCreateDto">Objeto con los datos de la categoría a crear.</param>
        /// <returns>La categoría creada.</returns>
        [HttpPost]
        [Authorize(Roles = "admin")] // Solo los administradores pueden crear categorías
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto categoryCreateDto)
        {
            try
            {
                if (categoryCreateDto == null)
                    return BadRequest("Los datos de la categoría no pueden ser nulos.");

                var createdCategory = await _categoryService.AddAsync(categoryCreateDto);
                return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.Id }, createdCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza una categoría existente.
        /// </summary>
        /// <param name="id">ID de la categoría a actualizar.</param>
        /// <param name="categoryUpdateDto">Objeto con los nuevos datos de la categoría.</param>
        /// <returns>La categoría actualizada o un mensaje de error si no se encuentra.</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")] // Solo los administradores pueden actualizar categorías
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryUpdateDto categoryUpdateDto)
        {
            try
            {
                if (categoryUpdateDto == null || id != categoryUpdateDto.Id)
                    return BadRequest("Los datos de la categoría son inválidos.");

                var updatedCategory = await _categoryService.UpdateAsync(categoryUpdateDto);
                if (updatedCategory == null)
                    return NotFound($"Categoría con ID {id} no encontrada.");

                return Ok(updatedCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina una categoría por su ID.
        /// </summary>
        /// <param name="id">ID de la categoría a eliminar.</param>
        /// <returns>Respuesta vacía si se elimina con éxito o un mensaje de error si no existe.</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")] // Solo los administradores pueden eliminar categorías
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var success = await _categoryService.DeleteAsync(id);
                if (!success)
                    return NotFound($"Categoría con ID {id} no encontrada.");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
    }
}