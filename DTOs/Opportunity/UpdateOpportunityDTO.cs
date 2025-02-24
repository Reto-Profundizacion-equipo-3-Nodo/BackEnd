namespace FundAntivirus.DTOs.Opportunity
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using FundAntivirus.Models;

    /// <summary>
    /// Representa la respuesta de una oportunidad en el sistema.
    /// </summary>
    public class UpdateOpportunityDTO
    {
        /// <summary>
        /// Identificador único de la oportunidad.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre de la oportunidad.
        /// </summary>
        [StringLength(255, MinimumLength = 5, ErrorMessage = "El nombre debe tener entre 5 y 255 caracteres.")]
        public required string Name { get; set; }

        /// <summary>
        /// Descripción de la oportunidad (opcional).
        /// </summary>
        [StringLength(500, ErrorMessage = "La descripción no puede superar los 500 caracteres.")]
        public string? Description { get; set; }

        /// <summary>
        /// Nombre de la categoría de la oportunidad.
        /// </summary>
        public required string CategoryName { get; set; }

        /// <summary>
        /// Nombre del usuario que creó la oportunidad.
        /// </summary>
        public required string CreatedBy { get; set; }

        /// <summary>
        /// Estado actual de la oportunidad.
        /// </summary>
        public OpportunityStatus Status { get; set; }

        /// <summary>
        /// Fecha y hora de creación de la oportunidad.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Fecha y hora de la última actualización de la oportunidad (opcional).
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}
