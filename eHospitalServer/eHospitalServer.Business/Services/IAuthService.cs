using CTS.Result;
using eHospitalServer.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eHospitalServer.Business.Services;
public interface IAuthService
{
    Task<Result<LoginResponseDto>> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken);
    Task<Result<LoginResponseDto>> GetTokenByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
    Task<Result<string>> SendConfirmEmailAsync(string email, CancellationToken cancellationToken);
    Task<Result<string>> ConfirmVerificationEmailAsync(int emailConfirmCode, CancellationToken cancellationToken);
    Task<Result<string>> SendPasswordResetCodeAsync(string email, CancellationToken cancellationToken);
    Task<Result<string>> ResetPasswordWithCodeAsync(int passwordResetCode, string newPassword,CancellationToken cancellationToken);
}
