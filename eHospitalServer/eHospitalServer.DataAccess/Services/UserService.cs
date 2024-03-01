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

        user.EmailConfirmCode = random.Next(100000, 999999);
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

        if (result.Succeeded)
        {
            string emailConfirmCode = user.EmailConfirmCode.ToString();
            string htmlCodeBoxes = string.Join("", emailConfirmCode.Select(c => $"<span class='code-box'>{c}</span>"));
            string emailBody = $@"
                <html>
                <head>
                <style>
                  .code-box {{
                    font-family: 'Arial', sans-serif;
                    font-size: 24px;
                    border: 1px solid #000;
                    display: inline-block;
                    width: 35px;
                    height: 35px;
                    margin: 5px;
                    line-height: 35px;
                    text-align: center;
                  }}
                </style>
                </head>
                <body>
                  <p>Your email confirmation code:</p>
                  <div>
                    {htmlCodeBoxes}
                  </div>
                  <p>Please note that this code will expire in 5 minutes.</p>
                </body>
                </html>";

            string response = await MailService.SendEmailAsync(user.Email ?? "", "eHospital - Email Confirmation", emailBody);

            return Result<string>.Succeed("User create is successful");
        }
        return Result<string>.Failure(500, result.Errors.Select(s => s.Description).ToList());
    }

    public async Task<Result<string>> ConfirmEmailAsync(string email, int confirmationCode)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
        {
            return Result<string>.Failure(StatusCodes.Status404NotFound, "User not found.");
        }

        if (user.EmailConfirmCode != confirmationCode)
        {
            return Result<string>.Failure(StatusCodes.Status400BadRequest, "Invalid code.");
        }

        if (user.EmailConfirmCodeSendDate.AddMinutes(5) < DateTime.UtcNow)
        {
            return Result<string>.Failure(StatusCodes.Status400BadRequest, "Code is expired.");
        }

        user.EmailConfirmed = true;
        var updateResult = await userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            return Result<string>.Failure(StatusCodes.Status500InternalServerError, "An error occurred while confirming email.");
        }

        return Result<string>.Succeed("Email confirmation is successful.");
    }
}

