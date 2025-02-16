using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FundAntivirus.Models;
using FundAntivirus.DTOs;
using FundAntivirus.Repositories;
using System;
using Swashbuckle.AspNetCore.Annotations;

namespace FundAntivirus.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        /// <summary> Obtiene todas las categorías. </summary>
        [HttpGet]
        [SwaggerOperation(Summary = "Obtiene todas las categorías", Description = "Devuelve una lista de categorías disponibles en la base de datos.")]
        [SwaggerResponse(200, "Lista de categorías obtenida exitosamente", typeof(IEnumerable<CategoryResponseDTO>))]
        [SwaggerResponse(500, "Error interno del servidor")]
        public async Task<ActionResult<IEnumerable<CategoryResponseDTO>>> GetCategories()
        {
            try
            {
                var categories = await _categoryRepository.GetAllCategories();
                return Ok(categories.Select(MapToDTO));
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary> Obtiene una categoría por su ID. </summary>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtiene una categoría por ID", Description = "Busca una categoría específica en la base de datos.")]
        [SwaggerResponse(200, "Categoría encontrada", typeof(CategoryResponseDTO))]
        [SwaggerResponse(404, "Categoría no encontrada")]
        [SwaggerResponse(500, "Error interno del servidor")]
        public async Task<ActionResult<CategoryResponseDTO>> GetCategoryById(int id)
        {
            try
            {
                var category = await _categoryRepository.GetCategoryById(id);
                return category == null ? NotFound(new { message = "Categoría no encontrada" }) : Ok(MapToDTO(category));
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary> Crea una nueva categoría. </summary>
        [HttpPost]
        [SwaggerOperation(Summary = "Crea una nueva categoría", Description = "Registra una nueva categoría en la base de datos.")]
        [SwaggerResponse(201, "Categoría creada correctamente", typeof(CategoryResponseDTO))]
        [SwaggerResponse(400, "Datos inválidos o categoría duplicada")]
        [SwaggerResponse(500, "Error interno del servidor")]
        public async Task<ActionResult<CategoryResponseDTO>> CreateCategory([FromBody] CategoryCreateDTO categoryDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                string normalizedName = categoryDto.Name.Trim().ToLower();
                if (string.IsNullOrWhiteSpace(normalizedName))
                    return BadRequest(new { message = "El nombre de la categoría no puede estar vacío." });

                if (await _categoryRepository.Exists(normalizedName))
                    return BadRequest(new { message = "Ya existe una categoría con ese nombre." });

                var newCategory = new Category
                {
                    Name = categoryDto.Name.Trim(),
                    Description = categoryDto.Description?.Trim()
                };

                var createdCategory = await _categoryRepository.CreateCategory(newCategory);
                return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.Id }, MapToDTO(createdCategory));
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary> Actualiza una categoría existente. </summary>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Actualiza una categoría", Description = "Modifica una categoría en la base de datos.")]
        [SwaggerResponse(200, "Categoría actualizada", typeof(CategoryResponseDTO))]
        [SwaggerResponse(400, "Datos inválidos o conflicto de ID")]
        [SwaggerResponse(404, "Categoría no encontrada")]
        [SwaggerResponse(500, "Error interno del servidor")]
        public async Task<ActionResult<CategoryResponseDTO>> UpdateCategory(int id, [FromBody] CategoryUpdateDTO categoryDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != categoryDto.Id) return BadRequest(new { message = "El ID proporcionado no coincide con el de la categoría." });

            try
            {
                var category = await _categoryRepository.GetCategoryById(id);
                if (category == null) return NotFound(new { message = "Categoría no encontrada" });

                if (!string.IsNullOrWhiteSpace(categoryDto.Name))
                {
                    string newName = categoryDto.Name.Trim().ToLower();
                    if (await _categoryRepository.Exists(newName) && category.Name.ToLower() != newName)
                        return BadRequest(new { message = "Ya existe una categoría con ese nombre." });

                    category.Name = categoryDto.Name.Trim();
                }

                category.Description = categoryDto.Description?.Trim() ?? category.Description;
                var updatedCategory = await _categoryRepository.UpdateCategory(category);

                return Ok(MapToDTO(updatedCategory));
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary> Elimina una categoría por su ID. </summary>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Elimina una categoría", Description = "Borra una categoría de la base de datos por su ID.")]
        [SwaggerResponse(204, "Categoría eliminada correctamente")]
        [SwaggerResponse(404, "Categoría no encontrada")]
        [SwaggerResponse(500, "Error interno del servidor")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var category = await _categoryRepository.GetCategoryById(id);
                if (category == null) return NotFound(new { message = "Categoría no encontrada" });

                await _categoryRepository.DeleteCategory(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return ServerError(ex);
            }
        }

        /// <summary> Convierte una entidad Category a CategoryResponseDTO. </summary>
        private static CategoryResponseDTO MapToDTO(Category category) => new()
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        };

        /// <summary> Devuelve un error 500 con el mensaje de la excepción. </summary>
        private ObjectResult ServerError(Exception ex) =>
            StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
    }
}
