using FundAntivirus.Models;
using System.ComponentModel.DataAnnotations;
using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace FundAntivirus.Tests.Models
{
    public class CategoryTests
    {
        /// <summary>
        /// Prueba unitaria para validar que el campo "Name" en la clase Category es obligatorio.
        /// </summary>
        [Fact]
        public void Category_Name_ShouldBeRequired()
        {
            // 🔹 Arrange: Se crea una instancia de Category con un nombre vacío para probar la validación
            var category = new Category
            {
                Name = "",
                Description = "Ejemplo"
            };

            var context = new ValidationContext(category);
            var results = new List<ValidationResult>();
            
            // 🔹 Act: Se valida el objeto con las reglas de anotaciones de datos (DataAnnotations)
            bool isValid = Validator.TryValidateObject(category, context, results, true);

             // 🔹 Debugging: Se imprimen los mensajes de error si la validación falla
            foreach (var error in results)
            {
                System.Diagnostics.Debug.WriteLine($"Mensaje de error: {error.ErrorMessage ?? "N/A"}");
            }

            // 🔹 Assert: Se verifica que la validación falle y que el mensaje de error esperado esté presente
            Assert.False(isValid); // La validación debe fallar
            Assert.Contains(results.Select(r => r.ErrorMessage ?? ""), 
                msg => msg.Contains("El nombre de la categoría es obligatorio."));
        }
    }
}