namespace DynamicProxyProject.Interceptors
{
    /// <summary>
    /// Перехватчик, который отнимает значение у общей переменной
    /// </summary>
    public class SubstractInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Constants.Number -= 7;
            invocation.Proceed();
        }
    }

    /// <summary>
    /// Перехватчик, который отнимает значение у общей переменной для обертки под Microsoft.Extensions.DependancyInjection
    /// </summary>
    public class SubstractDecorator : IDecorator
    {
        public Task OnInvoke(Call call)
        {
            Constants.Number -= 7;
            call.Next();
            return Task.CompletedTask;
        }
    }
}
