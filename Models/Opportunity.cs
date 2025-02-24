using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FundAntivirus.Models
{
    /// <summary>
    /// Representa una oportunidad dentro del sistema.
    /// </summary>
    public class Opportunity
    {
        /// <summary>
        /// Identificador único de la oportunidad (Clave primaria).
        /// Generado automáticamente por la base de datos.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Nombre de la oportunidad (Requerido, máximo 255 caracteres).
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descripción detallada de la oportunidad (Opcional).
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Identificador de la categoría a la que pertenece la oportunidad.
        /// (Clave foránea requerida).
        /// </summary>
        [Required]
        public int CategoryId { get; set; }

        /// <summary>
        /// Relación con la categoría asociada.
        /// </summary>
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; } = null!;

        /// <summary>
        /// Identificador del usuario que creó o gestiona la oportunidad.
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// Relación con el usuario asociado a la oportunidad.
        /// </summary>
        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;

        /// <summary>
        /// Estado actual de la oportunidad (Activo, Inactivo o Cerrado).
        /// Se almacena como texto en la base de datos.
        /// </summary>
        [Required]
        [EnumDataType(typeof(OpportunityStatus))]
        [Column(TypeName = "varchar(20)")]
        public OpportunityStatus Status { get; set; } = OpportunityStatus.Active;

        /// <summary>
        /// Información de auditoría que registra fechas de creación y modificación.
        /// </summary>
        public AuditInfo AuditInfo { get; set; } = new AuditInfo();

        /// <summary>
        /// Asigna un usuario a la oportunidad, validando que el ID sea válido.
        /// </summary>
        /// <param name="userId">Identificador del usuario</param>
        /// <exception cref="ArgumentException">Se lanza si el ID es inválido</exception>
        public void AssignUser(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("UserId no puede ser menor o igual a 0.");
            }
            UserId = userId;
        }
    }

    /// <summary>
    /// Representa los posibles estados de una oportunidad.
    /// </summary>
    public enum OpportunityStatus
    {
        Active,
        Inactive,
        Closed
    }

    /// <summary>
    /// Contiene información de auditoría para rastrear cambios en la entidad.
    /// </summary>
    public class AuditInfo
    {
        /// <summary>
        /// Fecha y hora de creación de la oportunidad.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Fecha y hora de la última modificación de la oportunidad.
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}