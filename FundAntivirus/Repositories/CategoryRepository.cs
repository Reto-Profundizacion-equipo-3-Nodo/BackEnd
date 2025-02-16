using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FundAntivirus.Data;
using FundAntivirus.Models;

namespace FundAntivirus.Repositories
{
    // Repositorio que implementa las operaciones para manejar categorías en la base de datos.
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        // Constructor que recibe el contexto de la base de datos.
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //verifica si existe una categoria con el nombre proporcionado en la base de datos.
        public async Task<bool> Exists(string name)
        {
            return await
            _context.Categories.AnyAsync(c => c.Name == name);
        }

        // Obtener todas las categorías.
        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        // Obtener una categoría por ID.
        public async Task<Category?> GetCategoryById(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        // Crear una nueva categoría.
        public async Task<Category> CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        // Actualizar una categoría existente.
        public async Task<Category> UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }

        // Eliminar una categoría por ID.
        public async Task<bool> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

