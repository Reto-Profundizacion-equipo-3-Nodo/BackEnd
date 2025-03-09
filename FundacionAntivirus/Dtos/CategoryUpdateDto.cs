using System.ComponentModel.DataAnnotations;

namespace FundacionAntivirus.Dtos;

/// <summary>
/// DTO para la actualización de una categoría.
/// </summary>
public class CategoryUpdateDto
{
    /// <summary>
    /// Identificador único de la categoría (obligatorio).
    /// </summary>
    [Required(ErrorMessage = "El Id de la categoría es obligatorio.")]
    public int Id { get; set; }

    /// <summary>
    /// Nombre de la categoría (obligatorio, máximo 255 caracteres).
    /// </summary>
    [Required(ErrorMessage = "El nombre de la categoría es obligatorio.")]
    [StringLength(255, ErrorMessage = "El nombre no puede superar los 255 caracteres.")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Descripción de la categoría (opcional).
    /// </summary>
    public string? Description { get; set; }
}