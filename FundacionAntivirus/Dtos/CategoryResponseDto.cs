namespace FundacionAntivirus.Dtos;

/// <summary>
/// DTO para la respuesta de una categoría.
/// </summary>
public class CategoryResponseDto
{
    /// <summary>
    /// Identificador único de la categoría.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre de la categoría.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Descripción de la categoría.
    /// </summary>
    public string? Description { get; set; }
}