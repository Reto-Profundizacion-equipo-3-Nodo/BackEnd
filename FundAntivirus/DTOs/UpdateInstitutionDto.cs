using System.ComponentModel.DataAnnotations;

namespace FundAntivirus.DTOs
{
    public class UpdateInstitutionDto
    {
        [MaxLength(255)]
        public string? Nombre { get; set; }

        public string? Ubicacion { get; set; }
        public string? UrlGeneralidades { get; set; }
        public string? UrlOfertaAcademica { get; set; }
        public string? UrlBienestar { get; set; }
        public string? UrlAdmision { get; set; }
    }
}
