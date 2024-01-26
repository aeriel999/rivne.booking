using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using rivne.booking.Core.DTOs.Apartments;
using rivne.booking.Core.Services;

namespace rivne.booking.api.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
[ApiController]
public class ApartmentController : Controller
{
	private readonly ApartmentService _apartmentService;

    public ApartmentController(ApartmentService apartmentService)
    {
		_apartmentService = apartmentService;

	}

	[HttpPost("addApartment")]
	public async Task<IActionResult> AddApartment(AddApartmentDto model)
	{
		var result = await _apartmentService.AddApartmentAsync(model);
		if (result.Success) return Ok(result);
		else return BadRequest(result.Message);
	}
	
	[HttpGet("getAll")]
	public async Task<IActionResult> GetAll()
	{
		var result = await _apartmentService.GetAllApattmentsAsync();

		if (result.Success) return Ok(result);
		else return BadRequest(result.Message);
	}

}
