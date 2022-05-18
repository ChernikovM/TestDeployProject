using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SS.DataAccess.Entities;
using SS.DataAccess.Entities.Base;
using SS.DataAccess.Repositories.EF;
using SS.DataAccess.Repositories.EF.Base;
using SS.DataAccess.Repositories.EF.Interfaces;

namespace SS.DataAccess;

public static class Extensions
{
    public static IServiceCollection RegisterDbContext(
        this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        return serviceCollection.AddDbContext<AppContext>(
            options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    }

    // ReSharper disable once InconsistentNaming
    public static IServiceCollection RegisterEFRepository<TService, TImplementation, TType>(this IServiceCollection serviceCollection)
        where TService : class, IEFRepository<TType>
        where TImplementation : class, TService
        where TType : EntityBase
    {
        return serviceCollection
            .AddScoped<TService, TImplementation>();
    }

    public static IServiceCollection RegisterDataAccessServices(
        this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        return serviceCollection
            .RegisterDbContext(configuration)
            .RegisterEFRepository<IStickerPackRepository, StickerPackRepository, StickerPack>()
            .RegisterEFRepository<ILabelRepository, LabelRepository, Label>();
    }
}