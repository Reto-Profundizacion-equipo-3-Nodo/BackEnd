using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FundacionAntivirus.Models;

public partial class Opportunities
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    public string? Observation { get; set; }

    [StringLength(50)]
    public string? Type { get; set; }

    public string? Description { get; set; }

    public string? Requires { get; set; }

    public string? Guide { get; set; }

    public string? Adicional_dates { get; set; }

    public string? ServiceChannels { get; set; }

    [StringLength(255)]
    public string? Manager { get; set; }

    [StringLength(50)]
    public string? Modality { get; set; }

    public int? CategoryId { get; set; }

    public int? InstitutionId { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Opportunities")]
    public virtual Categories? Category { get; set; }

    [ForeignKey("InstitutionId")]
    [InverseProperty("Opportunities")]
    public virtual Institution? Institution { get; set; }

    [InverseProperty("Opportunity")]
    public virtual ICollection<UserOpportunities> UserOpportunities { get; set; } = new List<UserOpportunities>();
}
