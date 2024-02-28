using eHospitalServer.Entities.Models;

namespace eHospitalServer.Entities.Abstractions;
public interface IJwtService
{
    string CreateToken(User user);
}
