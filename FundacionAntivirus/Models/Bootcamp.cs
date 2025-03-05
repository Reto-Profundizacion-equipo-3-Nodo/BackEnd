using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FundacionAntivirus.Models;

// public partial class Bootcamp
// {
//     [Key]
//     public int Id { get; set; }

//     [StringLength(255)]
//     public string Name { get; set; } = null!;

//     public string? Description { get; set; }

//     public string? Information { get; set; }

//     public string? Costs { get; set; }

//     public int? InstitutionId { get; set; }

//     [InverseProperty("Bootcamp")]
//     public virtual ICollection<BootcampTopic> BootcampTopics { get; set; } = new List<BootcampTopic>();

//     [ForeignKey("InstitutionId")]
//     [InverseProperty("Bootcamp")]
//     public virtual Institution? Institution { get; set; }
// }
public partial class Bootcamp
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Information { get; set; }

    public string? Costs { get; set; }

    public int? InstitutionId { get; set; }

    public virtual ICollection<BootcampTopic> BootcampTopics { get; set; } = new List<BootcampTopic>();

    [ForeignKey("InstitutionId")]
    public virtual Institution? Institution { get; set; }
}

