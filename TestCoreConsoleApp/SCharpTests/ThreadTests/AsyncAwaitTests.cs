using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SCharpTests.ThreadTests
{
    public class AsyncAwaitTests
    {
        [SetUp]
        public void Setup()
        {
            /*
             * ВЫВОД:
             * 1) Неважно сколько await в асинхронном методе, распаралеливание происходит всегда после первого await
             * 2) Если в async методе нет await, то смысла от async никакого нет
             * 3) Если асинхронгный метод вызывается без await, то не ждем результата, иначе ждем до самого конца
             */
        }

        [Test]
        public void NoAwait_OneAwait_Test()
        {
            Console.WriteLine($"Test Start! {DateTime.Now}");
            NoAwait_OneAwait_Async();
            Thread.Sleep(2000);
            Console.WriteLine($"Test End! {DateTime.Now}");
            Thread.Sleep(5000);
        }

        private async void NoAwait_OneAwait_Async()
        {
            Thread.Sleep(1000);
            Console.WriteLine($"MethodAsync Start! {DateTime.Now}");
            await Task.Run(() => Thread.Sleep(3000));
            Console.WriteLine($"MethodAsync End! {DateTime.Now}");
        }
        
        [Test]
        public void Wait_OneAwait_Test()
        {
            Console.WriteLine($"Test Start! {DateTime.Now}");
            Wait_OneAwait_Async().Wait();
            Thread.Sleep(2000);
            Console.WriteLine($"Test End! {DateTime.Now}");
            Thread.Sleep(5000);
        }

        private async Task Wait_OneAwait_Async()
        {
            Thread.Sleep(1000);
            Console.WriteLine($"MethodAsync Start! {DateTime.Now}");
            await Task.Run(() => Thread.Sleep(3000));
            Console.WriteLine($"MethodAsync End! {DateTime.Now}");
        }
        
        [Test]
        public async Task HasAwait_OneAwait_Test()
        {
            Console.WriteLine($"Test Start! {DateTime.Now}");
            await HasAwait_OneAwait_Async();
            Thread.Sleep(2000);
            Console.WriteLine($"Test End! {DateTime.Now}");
            Thread.Sleep(5000);
        }

        private async Task HasAwait_OneAwait_Async()
        {
            Thread.Sleep(1000);
            Console.WriteLine($"MethodAsync Start! {DateTime.Now}");
            await Task.Run(() => Thread.Sleep(3000));
            Console.WriteLine($"MethodAsync End! {DateTime.Now}");
        }
        
        [Test]
        public void NoAwait_TwoAwait_Test()
        {
            Console.WriteLine($"Test Start! {DateTime.Now}");
            NoAwait_TwoAwait_Async();
            Thread.Sleep(2000);
            Console.WriteLine($"Test End! {DateTime.Now}");
            Thread.Sleep(10000);
        }

        private async void NoAwait_TwoAwait_Async()
        {
            Thread.Sleep(1000);
            Console.WriteLine($"MethodAsync Start1 {DateTime.Now}");
            await Task.Run(() => Thread.Sleep(3000));
            Console.WriteLine($"MethodAsync End1! {DateTime.Now}");
            Console.WriteLine($"MethodAsync Start2 {DateTime.Now}");
            await Task.Run(() => Thread.Sleep(4000));
            Console.WriteLine($"MethodAsync End2! {DateTime.Now}");
        }
        
        [Test]
        public async Task HasAwait_TwoAwait_Test()
        {
            Console.WriteLine($"Test Start! {DateTime.Now}");
            await HasAwait_TwoAwait_Async();
            Thread.Sleep(2000);
            Console.WriteLine($"Test End! {DateTime.Now}");
            Thread.Sleep(7000);
        }

        private async Task HasAwait_TwoAwait_Async()
        {
            Thread.Sleep(1000);
            Console.WriteLine($"MethodAsync Start1 {DateTime.Now}");
            await Task.Run(() => Thread.Sleep(3000));
            Console.WriteLine($"MethodAsync End1! {DateTime.Now}");
            Console.WriteLine($"MethodAsync Start2 {DateTime.Now}");
            await Task.Run(() => Thread.Sleep(4000));
            Console.WriteLine($"MethodAsync End2! {DateTime.Now}");
        }
        
        
    }
}