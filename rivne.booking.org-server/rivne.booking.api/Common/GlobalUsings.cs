﻿global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using rivne.booking.api.Contracts.User;
global using MediatR;
global using MapsterMapper;
global using rivne.booking.api.Contracts.Authetication.Register;
global using rivne.booking.api.Contracts.Authetication.Confirmation;
global using rivne.booking.api.Contracts.Authetication.Login;
global using rivne.booking.api.Contracts.Authetication.RefreshToken;
global using rivne.booking.api.Contracts.User.GetUser;
global using Rivne.Booking.Application.Users.Update;
global using Rivne.Booking.Application.Users.Create;
global using Rivne.Booking.Application.Users.Delete;
global using Rivne.Booking.Application.Users.Edit;
global using Rivne.Booking.Application.Users.GetAllUsers;
global using Rivne.Booking.Application.Users.GetUser;
global using Rivne.Booking.Application.Users.SetAvatar;
global using FluentValidation;
global using Mapster;
global using Rivne.Booking.Domain.Users;
global using Rivne.Booking.Application.Authentication.Confirmation;
global using Rivne.Booking.Application.Authentication.Login;
global using Rivne.Booking.Application.Authentication.LogOut;
global using Rivne.Booking.Application.Authentication.RefreshToken;
global using Rivne.Booking.Application.Authentication.Register;
global using FluentValidation.AspNetCore;
global using rivne.booking.api.Common;
global using rivne.booking.api.Contracts.Apartment;
global using rivne.booking.api.Validation.Apartment;
global using rivne.booking.api.Validation.Authentication;
global using rivne.booking.api.Validation.User;
global using System.Security.Claims;
global using rivne.booking.api.Contracts.Authetication.ForgotPassword;
global using Rivne.Booking.Application.Authentication.ForgotPassword;
