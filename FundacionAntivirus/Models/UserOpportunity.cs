using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FundacionAntivirus.Models;

/// <summary>
/// Representa la relación entre usuarios y oportunidades.
/// </summary>
public partial class UserOpportunity
{
    /// <summary>
    /// Identificador único de la relación usuario-oportunidad.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Identificador del usuario que aplica.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Identificador de la oportunidad a la que aplica el usuario.
    /// </summary>
    public int OpportunityId { get; set; }

    /// <summary>
    /// Oportunidad asociada.
    /// </summary>
    [ForeignKey("OpportunityId")]
    public virtual Opportunity Opportunity { get; set; } = null!;

    /// <summary>
    /// Usuario asociado.
    /// </summary>
    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;
}
