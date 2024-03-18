using CTS.Result;
using eHospitalServer.Entities.DTOs;
using eHospitalServer.Entities.Models;

namespace eHospitalServer.Business.Services;
public interface IUserService
{
    Task<Result<string>> CreateUserAsync(CreateUserDto request, CancellationToken cancellationToken);
    Task<Result<string>>CreatePatientAsync(CreatePatientDto request, CancellationToken cancellationToken);
    Task<Result<List<User>>> GetAllDoctorsAsync(CancellationToken cancellationToken);
}
