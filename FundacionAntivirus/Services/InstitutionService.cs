using FundacionAntivirus.Data;
using FundacionAntivirus.Models;
using FundacionAntivirus.Dto;
using FundacionAntivirus.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace FundacionAntivirus.Services
{
    public class InstitutionService : IInstitutionService
    {
        private readonly AppDbContext _context;

        public InstitutionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Institution>> GetAllInstitutions()
        {
            return await _context.Institutions.ToListAsync();
        }

        public async Task<Institution?> GetInstitutionById(int id)
        {
            return await _context.Institutions.FindAsync(id);
        }

        public async Task<Institution> CreateInstitution(InstitutionDto institutionDto)
{
    var institution = new Institution
    {
        Nombre = institutionDto.Nombre,
        Ubicacion = institutionDto.Ubicacion,
        UrlGeneralidades = institutionDto.UrlGeneralidades,
        UrlOfertaAcademica = institutionDto.UrlOfertaAcademica,
        UrlBienestar = institutionDto.UrlBienestar,
        UrlAdmision = institutionDto.UrlAdmision
    };

    _context.Institutions.Add(institution);
    await _context.SaveChangesAsync();
    return institution;
}


        public async Task<Institution?> UpdateInstitution(int id, InstitutionDto institutionDto)
{
    var institution = await _context.Institutions.FindAsync(id);
    if (institution == null) return null;

    institution.Nombre = institutionDto.Nombre;
    institution.Ubicacion = institutionDto.Ubicacion;
    institution.UrlGeneralidades = institutionDto.UrlGeneralidades;
    institution.UrlOfertaAcademica = institutionDto.UrlOfertaAcademica;
    institution.UrlBienestar = institutionDto.UrlBienestar;
    institution.UrlAdmision = institutionDto.UrlAdmision;

    await _context.SaveChangesAsync();
    return institution;
}


        public async Task<bool> DeleteInstitution(int id)
        {
            var institution = await _context.Institutions.FindAsync(id);
            if (institution == null) return false;

            _context.Institutions.Remove(institution);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
