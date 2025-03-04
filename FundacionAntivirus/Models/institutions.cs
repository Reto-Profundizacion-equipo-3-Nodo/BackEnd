using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FundacionAntivirus.Models;

public partial class Institutions
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    public string? Ubication { get; set; }

    public string? UrlGeneralidades { get; set; }

    public string? UrlOfertaAcademica { get; set; }

    public string? UrlBienestar { get; set; }

    public string? UrlAdmision { get; set; }

    [InverseProperty("Institution")]
    public virtual ICollection<Bootcamps> Bootcamps { get; set; } = new List<Bootcamps>();

    [InverseProperty("Institution")]
    public virtual ICollection<Opportunities> Opportunities { get; set; } = new List<Opportunities>();
}
