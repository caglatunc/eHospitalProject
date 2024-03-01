using Microsoft.Extensions.DependencyInjection;

namespace eHospitalServer.DataAccess.Utilities;
public static class ServiceTool
{
    public static IServiceProvider ServiceProvider { get; private set; } = default!;

    public static IServiceCollection CreateServiceTool(this IServiceCollection services)
    {
        ServiceProvider = services.BuildServiceProvider();
        return services;
    }
}
