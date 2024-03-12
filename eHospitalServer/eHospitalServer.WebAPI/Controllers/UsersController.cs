using CTS.Result;
using eHospitalServer.Business.Services;
using eHospitalServer.Entities.DTOs;
using eHospitalServer.WebAPI.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eHospitalServer.WebAPI.Controllers;
public sealed class UsersController(
    IUserService userService) : ApiController
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create(CreateUserDto request, CancellationToken cancellationToken)
    {
       var response = await userService.CreateUserAsync(request, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    [AllowAnonymous]

    public async Task<IActionResult> FindPatientWithIdentityNumberAsync(string identityNumber, CancellationToken cancellationToken)
    {
        var response = await userService.FindPatientWithIdentityNumberAsync(identityNumber,cancellationToken);
    
        return StatusCode(response.StatusCode, response);
    }
}
