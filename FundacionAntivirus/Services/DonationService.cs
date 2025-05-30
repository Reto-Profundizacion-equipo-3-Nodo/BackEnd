using FundacionAntivirus.Data;
using FundacionAntivirus.Interfaces;
using FundacionAntivirus.Models;
using Microsoft.EntityFrameworkCore;

namespace FundacionAntivirus.Repositories;

public class DonationRepository : IDonationRepository
{
    private readonly AppDbContext _context;

    public DonationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Donation>?> GetAllDonations()
    {
        return await _context.Donations.ToListAsync();
    }

    public async Task<Donation?> GetByIdDonation(int id)
    {
        return await _context.Donations.FindAsync(id);
    }

    public async Task<Donation?> DeleteByIdDontion(int id)
    {
        var donation = await _context.Donations.FindAsync(id);
        if (donation == null)
            return null;

        _context.Donations.Remove(donation);
        await _context.SaveChangesAsync();
        return donation;
    }

    public async Task<Donation> CreateDonation(Donation donation)
    {
        _context.Donations.Add(donation);
        await _context.SaveChangesAsync();
        return donation;
    }

    public async Task<Donation?> UpdateDonation(Donation donation)
    {
        var existingDonation = await _context.Donations.FindAsync(donation.Id);
        if (existingDonation == null)
            return null;

        _context.Entry(existingDonation).CurrentValues.SetValues(donation);
        await _context.SaveChangesAsync();
        return donation;
    }
}
