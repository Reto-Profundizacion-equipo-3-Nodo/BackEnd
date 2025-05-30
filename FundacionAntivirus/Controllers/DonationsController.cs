using FundacionAntivirus.Models;
using FundacionAntivirus.Interfaces;
using FundacionAntivirus.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FundacionAntivirus.Controllers;

[ApiController]
[Route("donations")]
public class DonationsController : ControllerBase
{
    private readonly IDonationRepository _donationRepository;

    public DonationsController(IDonationRepository donationRepository)
    {
        _donationRepository = donationRepository;
    }

    /// <summary>
    /// Obtiene todas las donaciones.
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "admin")] // Solo los administradores pueden ver todas las donaciones
    public async Task<IActionResult> GetAllDonations()
    {
        var response = await _donationRepository.GetAllDonations();

        if (response is null)
        {
            return NotFound("No se encontró ninguna donación.");
        }

        return Ok(response);
    }

    /// <summary>
    /// Obtiene una donación por su ID.
    /// </summary>
    [HttpGet("{id}")]
    [Authorize(Roles = "admin,user")] // admin puede ver todas, User solo puede ver las suyas
    public async Task<IActionResult> GetByIdDonation(int id)
    {
        var response = await _donationRepository.GetByIdDonation(id);

        if (response is null)
        {
            return NotFound("No se encontró ninguna donación.");
        }

        // Validar que un usuario solo pueda ver sus propias donaciones
        if (User.IsInRole("User") && response.UserId.ToString() != User.Identity.Name)
        {
            return Forbid();
        }

        return Ok(response);
    }

    /// <summary>
    /// Crea una nueva donación.
    /// </summary>
    [HttpPost("create")]
    [Authorize(Roles = "admin,user")] // Tanto admin como User pueden donar
    public async Task<IActionResult> CreateDonation([FromBody] DonationDto donationDto)
    {
        if (donationDto.UserId <= 0)
        {
            return BadRequest(new ErrorViewModel
            {
                StatusCode = 400,
                Message = "Error: la donación no puede ser nula.",
                RequestId = HttpContext.TraceIdentifier
            });
        }
        var donation = new Donation
        {
            UserId = donationDto.UserId,
            DonorName = donationDto.DonorName,
            Amount = donationDto.Amount,
            PaymentMethod = donationDto.PaymentMethod
        };
        var response = await _donationRepository.CreateDonation(donation);

        return Ok(response);
    }

    /// <summary>
    /// Actualiza una donación existente.
    /// </summary>
    [HttpPut("actualizar/{id}")]
    [Authorize(Roles = "admin,user")] // admin puede actualizar cualquier donación, User solo las suyas
    public async Task<IActionResult> UpdateDonation(int id, [FromBody] DonationDto donationDto)
    {
        if (donationDto.UserId <= 0)
        {
            return BadRequest(new ErrorViewModel
            {
                StatusCode = 400,
                Message = "Error: el usuario de la donación no puede ser nulo.",
                RequestId = HttpContext.TraceIdentifier
            });
        }
        
        var response = await _donationRepository.GetByIdDonation(id);
        if (response is null)
        {
            return NotFound(new { message = "No se encontró la donación a actualizar." });
        }

        // Validar que un usuario solo pueda actualizar sus propias donaciones
        if (User.IsInRole("User") && response.UserId.ToString() != User.Identity.Name)
        {
            return Forbid();
        }

        var donation = new Donation
        {
            Id = id,
            UserId = donationDto.UserId,
            DonorName = donationDto.DonorName,
            Amount = donationDto.Amount,
            PaymentMethod = donationDto.PaymentMethod
        };
        
        var updatedResponse = await _donationRepository.UpdateDonation(donation);
        return Ok(updatedResponse);
    }

    /// <summary>
    /// Elimina una donación por su ID.
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")] // Solo los administradores pueden eliminar donaciones
    public async Task<IActionResult> DeleteDonationById(int id)
    {
        var response = await _donationRepository.DeleteByIdDontion(id);

        if (response is null)
        {
            return NotFound("No se encontró la donación a borrar.");
        }

        return Ok(response);
    }
}
