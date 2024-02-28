using eHospitalServer.Entities.Models;
using NuGet.Protocol.Plugins;

namespace eHospitalServer.Core.Utilities.JWT;
public interface ITokenHandler
{
    Token CreateToken(User user, List<OperationClaim> operationClaims);
}
