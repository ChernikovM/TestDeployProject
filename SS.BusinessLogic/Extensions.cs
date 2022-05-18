using Microsoft.Extensions.DependencyInjection;
using SS.BusinessLogic.Services;
using SS.BusinessLogic.Services.Interfaces;

namespace SS.BusinessLogic;

public static class Extensions
{
    public static IServiceCollection RegisterBusinessServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<ILabelsService, LabelsService>()
            .AddScoped<IStickersService, StickersService>();
    }
}