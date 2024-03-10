using eHospitalServer.Business.Services;
using eHospitalServer.Entities.Models;
using eHospitalServer.WebAPI.Abstractions;
using Microsoft.AspNetCore.Mvc;
using CTS.Result;
using Microsoft.AspNetCore.Authorization;

namespace eHospitalServer.WebAPI.Controllers;

public sealed class DoctorsController(
    IUserService userService) : ApiController
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllDoctors(CancellationToken cancellationToken)
    {
      Result<List<User>> response= await userService.GetAllDoctorsAsync(cancellationToken);
        
       return StatusCode(response.StatusCode, response);
    }
}
