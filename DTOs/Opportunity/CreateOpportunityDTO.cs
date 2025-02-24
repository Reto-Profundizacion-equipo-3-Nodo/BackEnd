namespace FundAntivirus.DTOs.Opportunity
{
    using System.ComponentModel.DataAnnotations;
    using FundAntivirus.Models;

    /// <summary>
    /// DTO para la creación de una nueva oportunidad en el sistema.
    /// </summary>
    public class CreateOpportunityDTO
    {
        /// <summary>
        /// Nombre de la oportunidad.
        /// </summary>
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "El nombre debe tener entre 5 y 255 caracteres.")]
        public required string Name { get; set; }

        /// <summary>
        /// Descripción de la oportunidad (opcional).
        /// </summary>
        [StringLength(500, ErrorMessage = "La descripción no puede superar los 500 caracteres.")]
        public string? Description { get; set; }

        /// <summary>
        /// Identificador de la categoría a la que pertenece la oportunidad.
        /// </summary>
        [Required(ErrorMessage = "El ID de la categoría es obligatorio.")]
        public int CategoryId { get; set; }

        /// <summary>
        /// Identificador del usuario que crea la oportunidad.
        /// </summary>
        [Required(ErrorMessage = "El ID del usuario es obligatorio.")]
        public int UserId { get; set; }

        /// <summary>
        /// Estado inicial de la oportunidad (por defecto: Activo).
        /// </summary>
        [Required(ErrorMessage = "El estado de la oportunidad es obligatorio.")]
        public OpportunityStatus Status { get; set; } = OpportunityStatus.Active;
    }
}