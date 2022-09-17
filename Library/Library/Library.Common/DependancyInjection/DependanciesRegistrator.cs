using System.Reflection;

namespace Library.Common.DependancyInjection
{
    public class DependanciesRegistrator : IDependanciesRegistrator
    {
        private DependanciesRegistrator()
        {

        }

        public static IDependanciesRegistrator CreateDependanciesRegistrator()
        {
            return new DependanciesRegistrator();
        }

        public void RegisterFromBaseType(Type baseType, Action<Type, Type> registerAction)
        {
            var types = Assembly.GetAssembly(baseType)?.GetTypes()
                .Where(t => t.BaseType == baseType);

            if (types == null)
            {
                return;
            }

            foreach (var type in types)
            {
                var iface = type.GetInterfaces().FirstOrDefault();

                if (iface == null)
                {
                    continue;
                }

                registerAction(iface, type);
            }
        }

        public void RegisterFromNamespaceInterfaces(Type assemblyType, string nspace, Action<Type, Type> registerAction)
        {
            var assembly = Assembly.GetAssembly(assemblyType);

            if(assembly == null)
            {
                return;
            }

            var ifaces = assembly.GetTypes().Where(t => t.IsInterface && t.Namespace != null && t.Namespace == nspace);

            if (ifaces == null)
            {
                return;
            }

            foreach (var iface in ifaces)
            {
                var name = iface.Name.Substring(1);

                var className = assembly.GetType($"{iface.Namespace}.{name}");

                if (className == null)
                {
                    continue;
                }

                registerAction(iface, className);
            }
        }
    }
}
