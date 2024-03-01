using CTS.Result;
using eHospitalServer.Business.Services;
using eHospitalServer.Entities.DTOs;
using eHospitalServer.Entities.Enums;
using eHospitalServer.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eHospitalServer.DataAccess.Services;
public sealed class UserService(
    UserManager<User> userManager) : IUserService
{
   public async Task<Result<string>> CreateUserAsync(CreateUserDto request, CancellationToken cancellationToken)
    {
        if(request.Email is not null)
        {
            bool isEmailExist = await userManager.Users.AnyAsync(p=>p.Email == request.Email);
            if(isEmailExist)
            {
                return Result<string>.Failure(400, "Email is already exist");
            }
        }


        User user = new()
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            BloodType = request.BloodType,
            DateOfBirth = request.DateOfBirth,
            FullAddress = request.FullAddress,
            IdentityNumber = request.IdentityNumber,
            PhoneNumber = request.PhoneNumber,
            UserName = request.UserName,
            UserType = request.UserType
        };

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

        if (result.Succeeded)
        {
            return Result<string>.Succeed("User create is successful");
        }

        return Result<string>.Failure(500, result.Errors.Select(s => s.Description).ToList());
    }
}
