using CTS.Result;
using eHospitalServer.Business.Services;
using eHospitalServer.DataAccess.Services;
using eHospitalServer.Entities.DTOs;
using eHospitalServer.Entities.Models;
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
    public async Task<IActionResult> Create(CreateAppointmentDto request, CancellationToken cancellationToken)
    {
        var response = await appointmentService.CreateAsync(request, cancellationToken);

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

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> FindPatientByIdentityNumber(FindPatientDto request, CancellationToken cancellationToken)
    {
        var response = await appointmentService.FindPatientByIdentityNumberAsync(request, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllDoctors(CancellationToken cancellationToken)
    {
        Result<List<User>> response = await appointmentService.GetAllDoctorsAsync(cancellationToken);

        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteById(Guid id, CancellationToken cancellationToken)
    {
        var response = await appointmentService.DeleteByIdAsync(id, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }
}
