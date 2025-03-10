using FundacionAntivirus.Data;
using FundacionAntivirus.Models;
using Microsoft.EntityFrameworkCore;
using FundacionAntivirus.Interfaces;

namespace FundacionAntivirus.Repository;

public class DonationRepository : IDonationRepository
{
    private readonly AppDbContext _context;

    public DonationRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Donation>?> GetAllDonations()
    {
        try
        {
            var donations = await _context.Donations.ToListAsync();
            
            return donations;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task<Donation?> GetByIdDonation(int id)
    {
        try
        {
            var donation = await _context.Donations.FirstOrDefaultAsync(x => x.Id == id);

            if (donation == null)
            {
                return null;
            }

            return donation;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Donation?> DeleteByIdDontion(int id)
    {
        try
        {
            var donation = await _context.Donations.FirstOrDefaultAsync(x => x.Id == id);

            if (donation == null)
            {
                return null;
            }
            
            _context.Donations.Remove(donation);
            await _context.SaveChangesAsync();

            return donation;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Donation> CreateDonation(Donation donation)
    {
        try
        {
            await _context.Donations.AddAsync(donation);
            await _context.SaveChangesAsync();
            return donation;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al crear la donación: {e.Message}");
            throw;
        }
    }

    public async Task<Donation?> UpdateDonation(Donation donation)
    {
        try
        {
            var existingDonation = await _context.Donations
                .FirstOrDefaultAsync(x => x.Id == donation.Id);

            if (existingDonation == null)
            {
                return null;
            }
            
            _context.Entry(existingDonation).CurrentValues.SetValues(donation);
            await _context.SaveChangesAsync();

            return existingDonation;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al actualizar la donación: {e.Message}");
            throw;
        }
    }
}