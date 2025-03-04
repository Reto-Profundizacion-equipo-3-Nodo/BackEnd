using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FundacionAntivirus.Models;

public partial class UserOpportunities
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    public int OpportunityId { get; set; }

    [ForeignKey("OpportunityId")]
    [InverseProperty("UserOpportunities")]
    public virtual Opportunities Opportunity { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("UserOpportunities")]
    public virtual Users User { get; set; } = null!;
}
