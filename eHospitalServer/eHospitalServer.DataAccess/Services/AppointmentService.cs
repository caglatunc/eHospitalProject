using AutoMapper;
using CTS.Result;
using eHospitalServer.Business.Services;
using eHospitalServer.Entities.DTOs;
using eHospitalServer.Entities.Enums;
using eHospitalServer.Entities.Models;
using eHospitalServer.Entities.Repositories;
using GenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace eHospitalServer.DataAccess.Services;
internal sealed class AppointmentService(
    UserManager<User> userManager,
    IAppointmentRepository appointmentRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IAppointmentService
{
    public async Task<Result<string>> CreateAppointmentAsync(CreateAppointmentDto request, CancellationToken cancellationToken)
    {
        // Check if the doctor exists
       User? doctor = await userManager.Users.Include(p=>p.DoctorDetail).FirstOrDefaultAsync(p => p.Id == request.DoctorId, cancellationToken);
        if(doctor is null || doctor.UserType != UserType.Doctor)
        {
              return Result<string>.Failure( 500,"Doctor not found.");
        }
        // Check doctor working days
        string day = request.StartDate.ToString("dddd");
        if (!doctor.DoctorDetail!.WorkingDays.Contains(day))
        {
            return Result<string>.Failure( 500,"Doctor not work on the requested day");
        }
        // Check doctor working hours. Patient can only make an appointment between doctor's working hours.
        DateTime startDate = DateTime.SpecifyKind(request.StartDate, DateTimeKind.Utc);
        DateTime endDate = DateTime.SpecifyKind(request.EndDate, DateTimeKind.Utc);

        bool isDoctorAvailable = !await appointmentRepository
                .GetWhere(p => p.DoctorId == request.DoctorId)
                .AnyAsync(p => (p.StartDate < endDate && p.StartDate >= startDate) ||
                               (p.EndDate > startDate && p.EndDate <= endDate) ||
                               (p.StartDate >= startDate && p.EndDate <= endDate) ||
                               (p.StartDate <= startDate && p.EndDate >= endDate), cancellationToken);

        if (!isDoctorAvailable)
        {
            return Result<string>.Failure( 500,"Doctor is not available on the requested date.");
        }
        // Created appointment and save to database
        Appointment appointment = mapper.Map<Appointment>(request);

        await appointmentRepository.AddAsync(appointment, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Appointment created successfully.");

    }
}