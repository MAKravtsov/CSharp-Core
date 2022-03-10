namespace DynamicProxyProject
{
    /// <summary>
    /// Класс создания прокси
    /// </summary>
    public class ProxyCreator
    {
        /// <summary>
        /// Создания прокси без DI
        /// </summary>
        public static void CleanCreate()
        {
            // объект создания прокси
            var generator = new ProxyGenerator();

            // интерфейс (обязательно!!! иначе ругается метод CreateInterfaceProxyWithTarget), который будет что-то делать
            IAddInterface addClass = new AddClass();

            /* 
             * динамические перехватчики
             * совершают промежуточные действия перед(после) вызова самого метода
             */
            IInterceptor addInterceptor = new AddInterceptor();
            IInterceptor substractInterceptor = new SubstractInterceptor();

            /*
             * добавление перехватчиков к классу
             * можно реализовать try catch в перехватчике
             * можно реализовать логирование
             * можно оборачивать в транзакцию
             * и тд
             */
            var addClassWithInterceptors = generator.CreateInterfaceProxyWithTarget(addClass, addInterceptor, substractInterceptor);

            // вызов метода класса (перехватчики будут работать в любом методе класса)
            addClassWithInterceptors.Add();
        }

        /// <summary>
        /// Создание прокси с DI Microsoft
        /// </summary>
        public static void CreateWithContainer()
        {
            // регситрация перехватчиков
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<AddDecorator>();
            serviceCollection.AddSingleton<SubstractDecorator>();

            // добавление класса с перехватчиками (не забыть методы пометить атрибутами)
            serviceCollection.AddDecor().AddSingleton<IAddInterface, AddClass>().Decorated();

            var container = serviceCollection.BuildServiceProvider();

            // вызов метода класса
            var addInterface = container.GetRequiredService<IAddInterface>();
            addInterface.Add();
        }

        /// <summary>
        /// Создание прокси с Autofac
        /// </summary>
        public static void CreateWithAutofac()
        {
            // регситрация перехватчиков
            var builder = new ContainerBuilder();
            builder.RegisterType<AddInterceptor>().SingleInstance();
            builder.RegisterType<SubstractInterceptor>().SingleInstance();

            // добавление класса с перехватчиками
            builder.RegisterType<AddClass>().As<IAddInterface>()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(AddInterceptor))
                .InterceptedBy(typeof(SubstractInterceptor));

            var container = builder.Build();

            // вызов метода класса
            var addInterface = container.Resolve<IAddInterface>();
            addInterface.Add();
        }
    }
}
