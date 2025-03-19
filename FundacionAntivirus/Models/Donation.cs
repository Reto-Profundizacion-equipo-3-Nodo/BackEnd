namespace FundacionAntivirus.Models;


public class Donation
{
    public int Id{get; set;}
    public double Amount{get; set;}
    public string DonorName { get; set; } = string.Empty;
    public DateTime Date{get; set;}
    public required string PaymentMethod{get; set;}
    //Relacion con usuario
    public int UserId{get; set;}
    public User User{get; set;}
    
}