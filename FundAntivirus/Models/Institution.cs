namespace FundAntivirus.Models
{
    public class Institution
    {
        public int Id { get; set; }
        
        public string Nombre { get; set; }
        
        public string? Ubicacion { get; set; }
        
        public string? UrlGeneralidades { get; set; }
        
        public string? UrlOfertaAcademica { get; set; }
        
        public string? UrlBienestar { get; set; }
        
        public string? UrlAdmision { get; set; }
    }
}