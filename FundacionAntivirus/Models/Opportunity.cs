using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FundacionAntivirus.Models;

/// <summary>
/// Representa una oportunidad dentro del sistema.
/// </summary>
[Table("opportunities")]
public partial class Opportunity
{
    /// <summary>
    /// Identificador único de la oportunidad.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Nombre de la oportunidad (obligatorio, máximo 255 caracteres).
    /// </summary>
    [Required(ErrorMessage = "El nombre de la oportunidad es obligatorio.")]
    [StringLength(255, ErrorMessage = "El nombre no puede superar los 255 caracteres.")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Observaciones adicionales sobre la oportunidad.
    /// </summary>
    public string? Observation { get; set; }

    /// <summary>
    /// Tipo de oportunidad (máximo 50 caracteres).
    /// </summary>
    [StringLength(50, ErrorMessage = "El tipo no puede superar los 50 caracteres.")]
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
    /// Fechas adicionales relacionadas con la oportunidad (máximo 255 caracteres).
    /// </summary>
    [StringLength(255, ErrorMessage = "Las fechas adicionales no pueden superar los 255 caracteres.")]
    public string? AdditionalDates { get; set; }

    /// <summary>
    /// Canales de servicio disponibles (máximo 255 caracteres).
    /// </summary>
    [StringLength(255, ErrorMessage = "Los canales de servicio no pueden superar los 255 caracteres.")]
    public string? ServiceChannels { get; set; }

    /// <summary>
    /// Nombre del responsable de la oportunidad (máximo 255 caracteres).
    /// </summary>
    [StringLength(255, ErrorMessage = "El nombre del responsable no puede superar los 255 caracteres.")]
    public string? Manager { get; set; }

    /// <summary>
    /// Modalidad de la oportunidad (máximo 50 caracteres).
    /// </summary>
    [StringLength(50, ErrorMessage = "La modalidad no puede superar los 50 caracteres.")]
    public string? Modality { get; set; }

    /// <summary>
    /// Identificador de la categoría a la que pertenece la oportunidad.
    /// </summary>
    public int? CategoryId { get; set; }

    /// <summary>
    /// Identificador de la institución que ofrece la oportunidad.
    /// </summary>
    public int? InstitutionId { get; set; }

    /// <summary>
    /// Categoría a la que pertenece esta oportunidad.
    /// </summary>
    [ForeignKey("CategoryId")]
    [InverseProperty("Opportunities")]
    public virtual Category? Category { get; set; }

    /// <summary>
    /// Institución que ofrece esta oportunidad.
    /// </summary>
    [ForeignKey("InstitutionId")]
    [InverseProperty("Opportunities")]
    public virtual Institution? Institution { get; set; }

    /// <summary>
    /// Lista de usuarios que han aplicado a esta oportunidad.
    /// </summary>
    [InverseProperty("Opportunity")]
    public virtual ICollection<UserOpportunities> UserOpportunities { get; set; } = new List<UserOpportunities>();
}