namespace FundAntivirus.DTOs.Opportunity
{
    using FundAntivirus.Models;

    /// <summary>
    /// DTO (Data Transfer Object) que representa la respuesta de una oportunidad.
    /// Se utiliza para enviar información sobre una oportunidad desde la API al cliente.
    /// </summary>
    public class OpportunityResponseDTO
    {
        /// <summary>
        /// Identificador único de la oportunidad.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre de la oportunidad.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descripción de la oportunidad.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Identificador de la categoría a la que pertenece la oportunidad.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Nombre de la categoría asociada.
        /// </summary>
        public string CategoryName { get; set; } = string.Empty;

        /// <summary>
        /// Identificador del usuario que creó la oportunidad.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Estado actual de la oportunidad.
        /// </summary>
        public OpportunityStatus Status { get; set; }
    }
}
