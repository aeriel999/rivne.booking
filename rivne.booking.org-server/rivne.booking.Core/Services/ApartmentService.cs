using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using rivne.booking.Core.DTOs.Apartments;
using rivne.booking.Core.Entities.Apartments;
using rivne.booking.Core.Entities.Users;
using rivne.booking.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rivne.booking.Core.Services;
public class ApartmentService
{
	 
	private readonly IMapper _mapper;
	private readonly IRepository<Apartment> _repository;
	public ApartmentService(IMapper mapper, IRepository<Apartment> repository)
    {
        _mapper = mapper;
		_repository = repository;
    }

	public async Task<ServiceResponse> AddApartmentAsync(AddApartmentDto model)
	{
		try
		{
			await _repository.Insert(_mapper.Map<Apartment>(model));
			await _repository.Save();

			return new ServiceResponse
			{
				Success = true,
				Message = "Appartment is save",
			};
		}
		catch (Exception ex)
		{
			return new ServiceResponse
			{
				Success = false,
				Message = ex.Message,
			};
		}
		
	}
	public async Task<ServiceResponse> GetAllApattmentsAsync()
    {
		var list = await _repository.GetAll();

		return new ServiceResponse
		{
			Success = true,
			PayLoad = list,
			Message = "Appartments are loaded",
		};
	}
}
