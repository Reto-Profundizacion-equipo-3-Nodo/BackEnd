namespace FundacionAntivirus.DTOs;

public class DonationDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string DonorName { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public double Amount { get; set; }
}
