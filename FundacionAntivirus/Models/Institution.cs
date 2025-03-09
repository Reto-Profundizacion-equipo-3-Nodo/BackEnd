using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using FundacionAntivirus.Models;

namespace FundacionAntivirus.Models;

public partial class Institution
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    public required string Nombre { get; set; }
    public required string Ubicacion { get; set; }
    public string? UrlGeneralidades { get; set; }
    public string? UrlOfertaAcademica { get; set; }
    public string? UrlBienestar { get; set; }
    public string? UrlAdmision { get; set; }

    [InverseProperty("Institution")]
    public virtual ICollection<Bootcamp> Bootcamps { get; set; } = new List<Bootcamp>();

    [InverseProperty("Institution")]
    public virtual ICollection<Opportunity> Opportunities { get; set; } = new List<Opportunity>();
}
