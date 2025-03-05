using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FundacionAntivirus.Models;

public partial class Categories
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<Opportunities> Opportunities { get; set; } = new List<Opportunities>();
}
