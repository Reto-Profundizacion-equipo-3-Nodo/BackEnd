using FundacionAntivirus.Data;
using FundacionAntivirus.Models;
using FundacionAntivirus.Interfaces;
using FundacionAntivirus.Dtos;
using Microsoft.EntityFrameworkCore;

namespace FundacionAntivirus.Repositories;

/// <summary>
/// Repositorio para la gestión de categorías en la base de datos.
/// Implementa las operaciones CRUD definidas en <see cref="ICategoryRepository"/>.
/// </summary>
public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Constructor que inicializa el contexto de base de datos.
    /// </summary>
    /// <param name="context">El contexto de la base de datos.</param>
    public CategoryRepository(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Obtiene todas las categorías disponibles en el sistema.
    /// </summary>
    /// <returns>Una lista de categorías.</returns>
    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Categories.AsNoTracking().ToListAsync();
    }

    /// <summary>
    /// Obtiene una categoría específica según su identificador.
    /// </summary>
    /// <param name="id">El ID de la categoría.</param>
    /// <returns>La categoría encontrada o null si no existe.</returns>
    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }

    /// <summary>
    /// Agrega una nueva categoría al sistema.
    /// </summary>
    /// <param name="category">Los datos de la categoría a agregar.</param>
    /// <returns>La categoría creada con su ID asignado.</returns>
    public async Task<Category> AddAsync(Category category)
    {
        if (category == null)
            throw new ArgumentNullException(nameof(category));

        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        return category;
    }

    /// <summary>
    /// Actualiza una categoría existente en el sistema.
    /// </summary>
    /// <param name="category">La categoría con los datos actualizados.</param>
    /// <returns>La categoría actualizada o null si no se encontró.</returns>
    public async Task<Category?> UpdateAsync(CategoryUpdateDto categoryUpdateDto)
    {
        if (categoryUpdateDto == null)
            throw new ArgumentNullException(nameof(categoryUpdateDto));

        var existingCategory = await _context.Categories.FindAsync(categoryUpdateDto.Id);
        if (existingCategory == null)
        {
            return null; // No se encontró la categoría.
        }
        // Actualiza los valores de la categoría existente con los nuevos valores.
        existingCategory.Name = categoryUpdateDto.Name;
        existingCategory.Description = categoryUpdateDto.Description;

        _context.Categories.Update(existingCategory);
        await _context.SaveChangesAsync();
        return existingCategory;
    }

    /// <summary>
    /// Elimina una categoría del sistema según su identificador.
    /// </summary>
    /// <param name="id">El ID de la categoría a eliminar.</param>
    /// <returns>True si la eliminación fue exitosa, false si no se encontró la categoría.</returns>
    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            return false; // Retorna false si la categoría no existe.
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return true;
    }
}