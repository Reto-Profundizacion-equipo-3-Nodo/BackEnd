namespace FundacionAntivirus.Dtos;

/// <summary>
/// DTO para la respuesta de una oportunidad.
/// </summary>
public class OpportunityResponseDto
{
    /// <summary>
    /// Identificador único de la oportunidad.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre de la oportunidad.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Observaciones adicionales sobre la oportunidad.
    /// </summary>
    public string? Observation { get; set; }

    /// <summary>
    /// Tipo de oportunidad.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Descripción general de la oportunidad.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Requisitos para acceder a la oportunidad.
    /// </summary>
    public string? Requires { get; set; }

    /// <summary>
    /// Guía de aplicación o información adicional.
    /// </summary>
    public string? Guide { get; set; }

    /// <summary>
    /// Fechas adicionales relacionadas con la oportunidad.
    /// </summary>
    public string? AdditionalDates { get; set; }

    /// <summary>
    /// Canales de servicio disponibles.
    /// </summary>
    public string? ServiceChannels { get; set; }

    /// <summary>
    /// Nombre del responsable de la oportunidad.
    /// </summary>
    public string? Manager { get; set; }

    /// <summary>
    /// Modalidad de la oportunidad.
    /// </summary>
    public string? Modality { get; set; }

    /// <summary>
    /// Identificador de la categoría.
    /// </summary>
    public int? CategoryId { get; set; }

    /// <summary>
    /// Nombre de la categoría asociada.
    /// </summary>
    public string? CategoryName { get; set; }

    /// <summary>
    /// Identificador de la institución.
    /// </summary>
    public int? InstitutionId { get; set; }
}