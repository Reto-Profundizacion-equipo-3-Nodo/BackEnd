using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FundacionAntivirus.Models;

/// <summary>
/// Representa una categoría dentro del sistema.
/// </summary>
[Table("categories")]
public class Category
{
    /// <summary>
    /// Identificador único de la categoría.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Nombre de la categoría. Es obligatorio y tiene un máximo de 255 caracteres.
    /// </summary>
    [Required(ErrorMessage = "El nombre de la categoría es obligatorio.")]
    [StringLength(255, ErrorMessage = "El nombre no puede tener más de 255 caracteres.")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Descripción de la categoría. Es opcional.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Colección de oportunidades asociadas a esta categoría.
    /// </summary>
    [InverseProperty("Category")]
    public virtual ICollection<Opportunity> Opportunities { get; set; } = new List<Opportunity>();
}