using Library.Common.DependancyInjection;
using Library.Common.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Domain.DI
{
    public class DiContainer
    {
        private readonly IServiceCollection _services;

        private readonly IDependanciesRegistrator _dependanciesRegistrator;

        public DiContainer(IServiceCollection services, IDependanciesRegistrator dependanciesRegistrator)
        {
            _services = services;
            _dependanciesRegistrator = dependanciesRegistrator;
        }

        public void Build()
        {
            _dependanciesRegistrator.RegisterFromNamespaceInterfaces(GetType(), 
                "Library.Domain.Catalog.Services", _services.GetAddScopedDelegate());
        }
    }
}
