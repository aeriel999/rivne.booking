using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using rivne.booking.api.Contracts.Apartment;
using rivne.booking.api.Contracts.Apartment.GetApartment;
using Rivne.Booking.Application.Apartaments.Create;
using Rivne.Booking.Application.Apartaments.Delete;
using Rivne.Booking.Application.Apartaments.GetAllApartments;
using Rivne.Booking.Application.Apartaments.GetApartment;
using Rivne.Booking.Application.Apartaments.GetStreetList;
using System.Security.Claims;

namespace rivne.booking.api.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
[ApiController]
public class ApartmentController(ISender mediatr, IMapper mapper) : Controller
{
	[AllowAnonymous]
	[HttpPost("addApartment")]
	public async Task<IActionResult> AddApartmentAsync([FromForm] CreateApartmentRequest request)
	{
		//ToDo ??? Validate here or in next step?
		//ToDo Maybe get user name here?
		string userId = User.Claims.First(u => u.Type == ClaimTypes.NameIdentifier).Value;

		//ToDo Make correct List Image mapping
		var command = mapper.Map<CreateApartmentCommand>((request, userId));

		var addApartmentResult = await mediatr.Send(command);

		return addApartmentResult.Match(
			refreshTokenResult => Ok(addApartmentResult),
			errors => Problem(errors[0].ToString()));
	}

	[AllowAnonymous]
	[HttpGet("getAll")]
	public async Task<IActionResult> GetAllApartmentsAsync()
	{
		var addApartmentResult = await mediatr.Send(new GetAllApartmentsQuery());

		return addApartmentResult.Match(
			refreshTokenResult => Ok(mapper.Map<List<ApartmentInfo>>(addApartmentResult.Value)),
			errors => Problem(errors[0].ToString()));

	}

	[AllowAnonymous]
	[HttpPost("deleteApartment")]
	public async Task<IActionResult> DeleteApartmentAsync([FromBody] DeleteApartamentRequest request)
	{
		var deleteApartmentResult = await mediatr.Send(mapper.Map<DeleteApartmentCommand>(request));

		return deleteApartmentResult.Match(
			refreshTokenResult => Ok(deleteApartmentResult),
			errors => Problem(errors[0].ToString()));
	}

	[AllowAnonymous]
	[HttpGet("getStreets")]
	public async Task<IActionResult> GetStreetNamesListAsync()
	{
		var getStreetNamesListResult = await mediatr.Send(new GetStreetNamesListQuery());

		return getStreetNamesListResult.Match(
			refreshTokenResult => Ok(getStreetNamesListResult.Value),
			errors => Problem(errors[0].ToString()));
	}

	[AllowAnonymous]
	[HttpGet("getApartment")]
	public async Task<IActionResult> GetApartment(int id)
	{
		//ToDo make another type of request
		//var getApartmentResult = await mediatr.Send(mapper.Map<GetAllApartmentsQuery>(request));

		var getApartmentResult = await mediatr.Send(new GetApatrmentQuery(id));

		return getApartmentResult.Match(
			refreshTokenResult => Ok(mapper.Map<GetApartementResponse>(getApartmentResult.Value)),
			errors => Problem(errors[0].ToString()));
	}

	[AllowAnonymous]
	[HttpPost("editApartment")]
	public async Task<IActionResult> EditApartment([FromForm] EditApartmentRequest request)
	{
		//ToDo ??? Maybe get a user ID and check it matches the apartment data?
		var editApartmentResult = await mediatr.Send(mapper.Map<DeleteApartmentCommand>(request));

		return editApartmentResult.Match(
			refreshTokenResult => Ok(editApartmentResult),
			errors => Problem(errors[0].ToString()));

	}
}
