using AutoMapper;
using CTS.Result;
using eHospitalServer.Business.Services;
using eHospitalServer.Entities.DTOs;
using eHospitalServer.Entities.Enums;
using eHospitalServer.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eHospitalServer.DataAccess.Services;
public sealed class UserService(
    UserManager<User> userManager,
    IMapper mapper) : IUserService
{
    public async Task<Result<string>> CreateUserAsync(CreateUserDto request, CancellationToken cancellationToken)
    {
        if(request.Email is not null)
        {
            bool isEmailExist = await userManager.Users.AnyAsync(p=>p.Email == request.Email);
            if(isEmailExist)
            {
                return Result<string>.Failure(StatusCodes.Status409Conflict, "Email is already exist.");
            }
        }

        if (request.UserName is not null)
        {
            bool isUserNameExist = await userManager.Users.AnyAsync(p => p.UserName == request.UserName);
            if (isUserNameExist)
            {
                return Result<string>.Failure(StatusCodes.Status409Conflict, "UserName is already exist.");
            }
        }

        if(request.IdentityNumber != "11111111111")
        {
            bool isIdentityNumberExist = await userManager.Users.AnyAsync(p => p.IdentityNumber == request.IdentityNumber);
            if (isIdentityNumberExist)
            {
                return Result<string>.Failure(StatusCodes.Status409Conflict, "IdentityNumber is already exist.");
            }
        }

        User user = mapper.Map<User>(request);

        Random random = new();

        bool isEmailConfirmCodeExists = true;
        while (isEmailConfirmCodeExists)
        {
            user.EmailConfirmCode = random.Next(100000, 999999);
            if (!userManager.Users.Any(p => p.EmailConfirmCode == user.EmailConfirmCode))
            {
                isEmailConfirmCodeExists = false;
            }
        }

        user.EmailConfirmCodeSendDate = DateTime.UtcNow;

        if (request.Specialty is not null)
        {
            user.DoctorDetail = new DoctorDetail()
            {
                Specialty = (Specialty)request.Specialty,
                WorkingDays = request.WorkingDays ?? new()
            };
        }

        IdentityResult result;

        if (request.Password is not null)
        {
            result = await userManager.CreateAsync(user, request.Password);
        }
        else
        {
            result = await userManager.CreateAsync(user);
        }

        if (!result.Succeeded)
        {
            return Result<string>.Failure(500, result.Errors.Select(s => s.Description).ToList());
        }

        return Result<string>.Succeed("User create is successful");
    }
    public async Task<Result<string>> CreatePatientAsync(CreatePatientDto request, CancellationToken cancellationToken)
    {
        if (request.Email is not null)
        {
            bool isEmailExist = await userManager.Users.AnyAsync(p => p.Email == request.Email);
            if (isEmailExist)
            {
                return Result<string>.Failure(StatusCodes.Status409Conflict, "Email is already exist.");
            }
        }

        if (request.IdentityNumber != "11111111111")
        {
            bool isIdentityNumberExist = await userManager.Users.AnyAsync(p => p.IdentityNumber == request.IdentityNumber);
            if (isIdentityNumberExist)
            {
                return Result<string>.Failure(StatusCodes.Status409Conflict, "IdentityNumber is already exist.");
            }
        }

        User user = mapper.Map<User>(request);
        user.UserType= UserType.Patient;
                
        Random random = new();

        bool isEmailConfirmCodeExists = true;
        while (isEmailConfirmCodeExists)
        {
            user.EmailConfirmCode = random.Next(100000, 999999);
            if (!userManager.Users.Any(p => p.EmailConfirmCode == user.EmailConfirmCode))
            {
                isEmailConfirmCodeExists = false;
            }
        }

        user.EmailConfirmCodeSendDate = DateTime.UtcNow;

        IdentityResult result = await userManager.CreateAsync(user);  

        if (!result.Succeeded)
        {
            return Result<string>.Failure(500, result.Errors.Select(s => s.Description).ToList());
        }

        return Result<string>.Succeed("Patient create is successful");
    }
    public async Task<Result<User>> FindPatientWithIdentityNumberAsync(string identityNumber, CancellationToken cancellationToken)
    {
        User? user = await userManager.FindByIdAsync(identityNumber);

        if (user is null)
        {
            return Result<User>.Failure(500, "User not found");
        }

        return user;
    }
}




