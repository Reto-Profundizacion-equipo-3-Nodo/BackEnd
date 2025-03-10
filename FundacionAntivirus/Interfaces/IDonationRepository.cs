using FundacionAntivirus.Models;

namespace FundacionAntivirus.Interfaces;


public interface IDonationRepository
{
    //GET
    Task<List<Donation>?> GetAllDonations();
    Task<Donation?> GetByIdDonation(int id);
    
    
    //DELETE
    Task<Donation?> DeleteByIdDontion(int id);
    
    //CREATE
    Task<Donation> CreateDonation(Donation donation);
    
    //UPDATE
    Task<Donation?> UpdateDonation(Donation donation);
}