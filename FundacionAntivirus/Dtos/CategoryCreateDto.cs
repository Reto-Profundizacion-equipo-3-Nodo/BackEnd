using System.ComponentModel.DataAnnotations;

namespace FundacionAntivirus.Dtos;

/// <summary>
/// DTO para la creación de una categoría.
/// </summary>
public class CategoryCreateDto
{
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