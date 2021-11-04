using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Interfaces.Interfaces;
using Microsoft.Extensions.Hosting;
using Timer = System.Timers.Timer;

namespace ActivityLogger
{
    public class Scheduller : BackgroundService
    {
        private IServiceProvider ServiceProvider;
        private Timer _timer;
        
        
        public Scheduller(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(2000);
            _timer.Elapsed += PollEvents;
            _timer.Start();
            return Task.CompletedTask;
        }

        private void PollEvents(Object source, ElapsedEventArgs e)
        {
            try
            {
                _timer.Stop();
                var logger = ServiceProvider.GetService(typeof(IActivityLogger)) as ActivityLoggerImpl;
                logger.ReceiveEvents();
            }
            catch
            {

            }
            
            _timer.Start();
        }
    }
}