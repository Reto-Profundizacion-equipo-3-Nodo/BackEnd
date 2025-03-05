using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FundacionAntivirus.Models;

public partial class UserOpportunity
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    public int OpportunityId { get; set; }

    [ForeignKey("OpportunityId")]
    [InverseProperty("UserOpportunity")]
    public virtual Opportunity Opportunity { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("UserOpportunity")]
    public virtual User User { get; set; } = null!;
}
