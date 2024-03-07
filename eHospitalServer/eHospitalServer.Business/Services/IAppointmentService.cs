using CTS.Result;
using eHospitalServer.Entities.DTOs;

namespace eHospitalServer.Business.Services;
public interface IAppointmentService
{
    Task<Result<string>> CreateAppointmentAsync(CreateAppointmentDto request, CancellationToken cancellationToken);
}
