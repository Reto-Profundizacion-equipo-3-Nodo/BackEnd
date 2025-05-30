using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FundacionAntivirus.Models;

[Index("Email", Name = "users_email_key", IsUnique = true)]
public partial class User
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    public string Email { get; set; } = null!;

    [StringLength(255)]
    public string Password { get; set; } = null!;

    [StringLength(50)]
    public string Rol { get; set; } = null!;
    [Required]
    [Phone]
    public string Celular { get; set; } = null!;

    [Required]
    public DateTime FechaNacimiento { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<UserOpportunity> UserOpportunities { get; set; } = new List<UserOpportunity>();
    //Relacion con donaciones
    public virtual ICollection<Donation> Donations { get; set; } = new List<Donation>();

}
