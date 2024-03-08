﻿using eHospitalServer.Business.Services;
using eHospitalServer.DataAccess.Services;
using eHospitalServer.Entities.DTOs;
using eHospitalServer.WebAPI.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eHospitalServer.WebAPI.Controllers;
public sealed class AppointmentsController(
    IUserService userService,
    IAppointmentService appointmentService) : ApiController
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreatePatient(CreatePatientDto request, CancellationToken cancellationToken)
    {
        var response = await userService.CreatePatientAsync(request, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreateAppointment(CreateAppointmentDto request, CancellationToken cancellationToken)
    {
        var response = await appointmentService.CreateAppointmentAsync(request, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CompleteAppointment(CompleteAppointmentDto request, CancellationToken cancellationToken)
    {
        var response = await appointmentService.CompleteAppointmentAsync(request, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllAppointmentByDoktorIdAsync(Guid doctorId, CancellationToken cancellationToken)
    {
        var response = await appointmentService.GetAllAppointmentByDoktorIdAsync(doctorId, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }
}
