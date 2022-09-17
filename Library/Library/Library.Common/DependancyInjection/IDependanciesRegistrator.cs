namespace Library.Common.DependancyInjection
{
    public interface IDependanciesRegistrator
    {
        void RegisterFromBaseType(Type baseType, Action<Type, Type> registerAction);

        void RegisterFromNamespaceInterfaces(Type assemblyType, string nspace, Action<Type, Type> registerAction);
    }
}
