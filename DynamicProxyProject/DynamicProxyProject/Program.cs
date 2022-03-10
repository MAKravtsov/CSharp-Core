// See https://aka.ms/new-console-template for more information

// библиотека для работы с перехватчиками
global using Castle.DynamicProxy;

global using DynamicProxyProject.Classes;
global using DynamicProxyProject.Interceptors;

// Microsoft DI и обертка для работы с перехвачиками
global using Decor; // содержит в себе Castle.Core
global using Microsoft.Extensions.DependencyInjection;

// Autofac
global using Autofac;
global using Autofac.Extras.DynamicProxy; // содержит в себе Castle.Core

using DynamicProxyProject;

//ProxyCreator.CleanCreate();
//ProxyCreator.CreateWithContainer();
ProxyCreator.CreateWithAutofac();

Console.WriteLine(Constants.Number);
Console.ReadLine();

public class Constants
{
    public static int Number { get; set; }
}
