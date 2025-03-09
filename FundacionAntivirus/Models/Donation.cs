namespace FundAntivirus.Models;

public class Donation
{
    public int Id{get; set;}
    public int UserId{get; set;}
    public double Amount{get; set;}
    public DateTime Date{get; set;}
    public required string PaymentMethod{get; set;}
    
}