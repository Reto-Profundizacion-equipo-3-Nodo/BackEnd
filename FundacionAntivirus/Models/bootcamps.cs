using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FundacionAntivirus.Models;

public partial class Bootcamps
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Information { get; set; }

    public string? Costs { get; set; }

    public int? InstitutionId { get; set; }

    [InverseProperty("Bootcamp")]
    public virtual ICollection<BootcampTopics> BootcampTopics { get; set; } = new List<BootcampTopics>();

    [ForeignKey("InstitutionId")]
    [InverseProperty("Bootcamps")]
    public virtual Institutions? Institution { get; set; }
}
