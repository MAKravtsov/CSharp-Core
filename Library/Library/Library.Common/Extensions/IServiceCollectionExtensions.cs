using Microsoft.Extensions.DependencyInjection;

namespace Library.Common.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static Action<Type, Type> GetAddScopedDelegate(this IServiceCollection serviceCollection)
        {
            return (a, b) => serviceCollection.AddScoped(a, b);
        }

        public static Action<Type, Type> GetAddSingletonDelegate(this IServiceCollection serviceCollection)
        {
            return (a, b) => serviceCollection.AddSingleton(a, b);
        }
    }
}
