using System.ComponentModel.DataAnnotations;

namespace FundacionAntivirus.Dto
{
    public class InstitutionDto
    {
        [Required]
        [MaxLength(255)]
        public required string Nombre { get; set; }

        public string? Ubicacion { get; set; }
        public string? UrlGeneralidades { get; set; }
        public string? UrlOfertaAcademica { get; set; }
        public string? UrlBienestar { get; set; }
        public string? UrlAdmision { get; set; }
    }
}
