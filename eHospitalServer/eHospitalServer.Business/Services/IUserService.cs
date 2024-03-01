using CTS.Result;
using eHospitalServer.Entities.DTOs;

namespace eHospitalServer.Business.Services;
public interface IUserService
{
    Task<Result<string>> CreateUserAsync(CreateUserDto request, CancellationToken cancellationToken);
    Task<Result<string>> ConfirmEmailAsync(string email, int confirmationCode);
}
