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
    public string Nombre { get; set; }
    public string Ubicacion { get; set; }
    public string? UrlGeneralidades { get; set; }
    public string? UrlOfertaAcademica { get; set; }
    public string? UrlBienestar { get; set; }
    public string? UrlAdmision { get; set; }

    [InverseProperty("institution")]
    public virtual ICollection<bootcamps> bootcamps { get; set; } = new List<bootcamps>();

    [InverseProperty("institution")]
    public virtual ICollection<opportunities> opportunities { get; set; } = new List<opportunities>();
}
