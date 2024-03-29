﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
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
	public async Task<IActionResult> AddApartment([FromForm] AddApartmentDto model)
	{
		string id = User.Claims.First().Value;

		var result = await _apartmentService.AddApartmentAsync(model, id);

		if (result.Success) return Ok(result);
		else return BadRequest(result.Message);
	}

	[HttpGet("getAll")]
	public async Task<IActionResult> GetAll()
	{
		var result = await _apartmentService.GetAllApattmentsAsync();

		if (result.Success)
		{
			return Ok(result);
		}
		else
		{
			return BadRequest(result.Message);
		}
	}

	[HttpPost("deleteApartment")]
	public async Task<IActionResult> DeleteUser([FromBody] int id)
	{
		var result = await _apartmentService.DeleteApartmentAsync(id);

		if (result.Success) return Ok(result);
		else return BadRequest(result.Message);
	}

	[HttpGet("getStreets")]
	public async Task<IActionResult> GetStreets()
	{
		var result = await _apartmentService.GetStreetsAsync();

		if (result.Success)
		{
			return Ok(result);
		}
		else
		{
			return BadRequest(result.Message);
		}
	}
 
	[HttpGet("getApartment")]
	public async Task<IActionResult> GetApartment(int apartmentId)
	{
		var result = await _apartmentService.GetApartmentAsync(apartmentId);

		if (result.Success)
		{
			return Ok(result);
		}
		return BadRequest(result);
	}

	[HttpPost("editApartment")]
	public async Task<IActionResult> EditApartment(EditApartment model)
	{
		var result = await _apartmentService.EditApartmentAsync(model);

		if (result.Success)
		{
			return Ok(result);
		}
		return BadRequest(result);
	}
}
