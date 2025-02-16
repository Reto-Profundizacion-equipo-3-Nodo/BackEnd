using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FundAntivirus.Models
{
    /// <summary>
    /// Representa una categoría dentro del sistema.
    /// </summary>
    [Table("categorias")] // Define el nombre de la tabla en la base de datos
    public class Category
    {
        /// <summary>
        /// Identificador único de la categoría.
        /// </summary>
        [Key] // Clave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Generación automática del ID
        public int Id { get; set; }

        /// <summary>
        /// Nombre de la categoría (obligatorio, hasta 100 caracteres).
        /// Ajusta el mensaje de error para que coincida con el que espera tu test.
        /// </summary>
        [Required(ErrorMessage = "El nombre de la categoría es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre del campo no puede exceder los 100 caracteres.")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descripción de la categoría (opcional, hasta 255 caracteres).
        /// </summary>
        [StringLength(255, ErrorMessage = "La descripción no puede exceder los 255 caracteres.")]
        public string? Description { get; set; }
    }
}