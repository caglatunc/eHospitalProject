using eHospitalServer.Entities.Abstractions;
using eHospitalServer.Entities.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace eHospitalServer.DataAccess.Services;
internal sealed class JwtService : IJwtService
{
    public string CreateToken(User user)
    {
        JwtSecurityToken jwtSecurityToken = new(
                        issuer: "Cagla Tunc Savas",
                        audience: "eHospital",
                        claims: null,
                        notBefore: DateTime.Now,
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my secret key my secret key my secret key 1234...my secret key my secret key my secret key 1234...")), SecurityAlgorithms.HmacSha512));

        JwtSecurityTokenHandler handler = new();
        string token = handler.WriteToken(jwtSecurityToken);

        return token;
    }
}
