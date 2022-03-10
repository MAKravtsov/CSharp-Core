namespace DynamicProxyProject.Interceptors
{
    /// <summary>
    /// Перехватчик, который добавляет значение общей переменной
    /// </summary>
    public class AddInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Constants.Number += 3;
            invocation.Proceed();
        }
    }

    /// <summary>
    /// Перехватчик, который добавляет значение общей переменной для обертки под Microsoft.Extensions.DependancyInjection
    /// </summary>
    public class AddDecorator : IDecorator
    {
        public Task OnInvoke(Call call)
        {
            Constants.Number += 3;
            call.Next();
            return Task.CompletedTask;
        }
    }
}
