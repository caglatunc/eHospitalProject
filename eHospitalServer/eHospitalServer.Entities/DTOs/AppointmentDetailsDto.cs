using eHospitalServer.Entities.Enums;

namespace eHospitalServer.Entities.DTOs;
public sealed record AppointmentDetailsDto(
    Guid AppointmentId,
    string DoctorName,
    string DoctorSpecialty,
    DateTime StartDate,
    DateTime EndDate,
    bool IsItFinished);


