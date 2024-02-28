using eHospitalServer.Entities.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace eHospitalServer.Core.Utilities.JWT;
public sealed class TokenHandler(IConfiguration configuration) : ITokenHandler
{
    public Token CreateToken(User user, List<OperationClaim> operationClaims)
    {
        Token token = new Token();

        //Security Key'in simetriğini alalım
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]));

        //Şifrelenmiş kimliği oluşturuyoruz
        SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        //Token ayarlarını yapıyoruz
        token.Expiration = DateTime.Now.AddMinutes(60);
        JwtSecurityToken securityToken = new JwtSecurityToken(
            issuer: configuration["Token:Issuer"],
            audience: configuration["Token:Audience"],
            expires: token.Expiration,
            claims: SetClaims(user, operationClaims),
            notBefore: DateTime.Now,
            signingCredentials: signingCredentials
            );

        //Token oluşturucu sınıfından bir örnek alalım
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        //Token üretelim
        token.AccessToken = jwtSecurityTokenHandler.WriteToken(securityToken);

        //Refresh token üretelim
        token.RefreshToken = CreateRefreshToken();
        return token;
    }

    public string CreateRefreshToken()
    {
        byte[] number = new byte[32];
        using (RandomNumberGenerator random = RandomNumberGenerator.Create())
        {
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }

    private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
    {
        var claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        claims.Add(new Claim(ClaimTypes.Name, user.FullName));
        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
        claims.Add(new Claim(ClaimTypes.Email, user.Email));
        claims.Add(new Claim("UserType", user.UserType.ToString()));

        return claims;
    }
}

