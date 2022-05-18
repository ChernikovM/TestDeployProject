using Microsoft.Extensions.DependencyInjection;
using SS.Parser.Base;
using SS.Parser.Chpic;

namespace SS.Parser;

public static class Extensions
{
    public static IServiceCollection RegisterDbUpdater<TUpdater>(this IServiceCollection serviceCollection)
        where TUpdater : class, IDbUpdater
    {
        return serviceCollection.AddTransient<IDbUpdater, TUpdater>();
    }

    public static IServiceCollection RegisterParser<TParser>(this IServiceCollection serviceCollection)
        where TParser : class, IParserBase

    {
        return serviceCollection.AddTransient<IParserBase, TParser>();
    }

    public static IServiceCollection RegisterParserServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .RegisterDbUpdater<DbUpdater>()
            .RegisterParser<ChpicParser>();
    }
}