namespace FundAntivirus.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Representa una categoría dentro del sistema.
    /// </summary>
    [Table("categorias")] // 🔹 Define el nombre de la tabla en la base de datos
    public class Category
    {
        /// <summary>
        /// Identificador único de la categoría.
        /// </summary>
        [Key] // 🔹 Define la clave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // 🔹 Generación automática del ID
        public int Id { get; set; }

        /// <summary>
        /// Nombre de la categoría (obligatorio, con un máximo de 100 caracteres).
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descripción de la categoría (opcional, con un máximo de 255 caracteres).
        /// </summary>
        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(255, ErrorMessage = "La descripción no puede tener más de 255 caracteres.")]
        public string? Description { get; set; } // 🔹 Se permite nulo, ya que algunas categorías podrían no tener descripción
    }
}


