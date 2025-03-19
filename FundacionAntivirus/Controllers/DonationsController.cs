using FundacionAntivirus.Models;
using FundacionAntivirus.Interfaces;
using FundacionAntivirus.DTOs;
using Microsoft.AspNetCore.Mvc;

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
    /// <remarks>
    /// Este endpoint devuelve todas las donaciones disponibles en la base de datos.
    /// </remarks>
    /// <response code="200">Devuelve la lista de donaciones</response>
    /// <response code="404">Si no se encuentran donaciones</response>
    [HttpGet]
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
    /// <param name="id">ID de la donación</param>
    /// <remarks>
    /// Este endpoint devuelve una donación específica según el ID proporcionado.
    /// </remarks>
    /// <response code="200">Devuelve la donación solicitada</response>
    /// <response code="404">Si no se encuentra la donación con el ID dado</response>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdDonation(int id)
    {
        var response = await _donationRepository.GetByIdDonation(id);

        if (response is null)
        {
            return NotFound("No se encontró ninguna donación.");
        }

        return Ok(response);
    }

    /// <summary>
    /// Elimina una donación por su ID.
    /// </summary>
    /// <param name="id">ID de la donación a eliminar</param>
    /// <remarks>
    /// Este endpoint elimina la donación específica según el ID proporcionado.
    /// </remarks>
    /// <response code="200">Devuelve la donación eliminada</response>
    /// <response code="404">Si no se encuentra la donación con el ID dado</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDonationById(int id)
    {
        var response = await _donationRepository.DeleteByIdDontion(id);

        if (response is null)
        {
            return NotFound("No se encontró la donación a borrar.");
        }

        return Ok(response);
    }

    /// <summary>
    /// Crea una nueva donación.
    /// </summary>
    /// <remarks>
    /// Este endpoint permite crear una nueva donación en el sistema.
    /// </remarks>
    /// <response code="200">Devuelve la donación creada</response>
    /// <response code="400">Si la donación no es válida</response>
    [HttpPost("create")]
    public async Task<IActionResult> CreateDonation([FromBody] DonationDto donationDto)
    {
        if (donationDto.UserId <= 0)
        {
            return StatusCode(400, new ErrorViewModel
            {
                StatusCode = 400,
                Message = "Error la donación no puede ser nula.",
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
    /// <remarks>
    /// Este endpoint permite actualizar una donación existente en el sistema.
    /// </remarks>
    /// <response code="200">Devuelve la donación actualizada</response>
    /// <response code="404">Si no se encuentra la donación a actualizar</response>
    [HttpPut("actualizar/{id}")]
    public async Task<IActionResult> UpdateDonation(int id, [FromBody] DonationDto donationDto)
    {
        if (donationDto.UserId <= 0)
        {
            return StatusCode(400, new ErrorViewModel
            {
                StatusCode = 400,
                Message = "Error la donación el user no puede ser nulo.",
                RequestId = HttpContext.TraceIdentifier
            });
        }
        var donation = new Donation
        {
            Id = donationDto.Id,
            UserId = donationDto.UserId,
            DonorName = donationDto.DonorName,
            Amount = donationDto.Amount,
            PaymentMethod = donationDto.PaymentMethod
        };
        
        var response = await _donationRepository.UpdateDonation(donation);
        if (response is null)
        {
            return StatusCode(400, new ErrorViewModel
            {
                StatusCode = 400,
                Message = "No se encontró la donación a actualizar.",
                RequestId = HttpContext.TraceIdentifier
            });
        }

        return Ok(response);
    }
}