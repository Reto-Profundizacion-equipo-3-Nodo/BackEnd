using FundAntivirus.Data;
using FundAntivirus.DTOs;
using FundAntivirus.Models;
using Microsoft.EntityFrameworkCore;

namespace FundAntivirus.Services
{
    public class InstitutionService : IInstitutionService
    {
        private readonly ApplicationDbContext _context;

        public InstitutionService(ApplicationDbContext context)
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

        public async Task<Institution> CreateInstitution(CreateInstitutionDto institutionDto)
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

        public async Task<Institution?> UpdateInstitution(int id, UpdateInstitutionDto institutionDto)
        {
            var institution = await _context.Institutions.FindAsync(id);
            if (institution == null) return null;

            institution.Nombre = institutionDto.Nombre ?? institution.Nombre;
            institution.Ubicacion = institutionDto.Ubicacion ?? institution.Ubicacion;
            institution.UrlGeneralidades = institutionDto.UrlGeneralidades ?? institution.UrlGeneralidades;
            institution.UrlOfertaAcademica = institutionDto.UrlOfertaAcademica ?? institution.UrlOfertaAcademica;
            institution.UrlBienestar = institutionDto.UrlBienestar ?? institution.UrlBienestar;
            institution.UrlAdmision = institutionDto.UrlAdmision ?? institution.UrlAdmision;

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
