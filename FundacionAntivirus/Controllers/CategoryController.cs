using FundacionAntivirus.Interfaces;
using FundacionAntivirus.Models;
using Microsoft.AspNetCore.Mvc;

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
        /// <param name="category">Objeto con los datos de la categoría a crear.</param>
        /// <returns>La categoría creada.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
        {
            try
            {
                if (category == null)
                    return BadRequest("Los datos de la categoría no pueden ser nulos.");

                var createdCategory = await _categoryService.AddAsync(category);
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
        /// <param name="category">Objeto con los nuevos datos de la categoría.</param>
        /// <returns>La categoría actualizada o un mensaje de error si no se encuentra.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category category)
        {
            try
            {
                if (category == null || id != category.Id)
                    return BadRequest("Los datos de la categoría son inválidos.");

                var updatedCategory = await _categoryService.UpdateAsync(category);
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