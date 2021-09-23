using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpsService.Interceptors
{
    public class LoggingInterceptor : Interceptor
    {
        private ILogger<LoggingInterceptor> logger;

        public LoggingInterceptor(ILogger<LoggingInterceptor> logger)
        {
            this.logger = logger;
        }

        public override async Task DuplexStreamingServerHandler<TRequest, TResponse>(IAsyncStreamReader<TRequest> requestStream
            , IServerStreamWriter<TResponse> responseStream
            , ServerCallContext context
            , DuplexStreamingServerMethod<TRequest, TResponse> continuation)
        {
            // обычный контекст в http запрсах webApi
            var _context = context.GetHttpContext();

            logger.Log(LogLevel.Information, $"Start - {context.Method}");

            await base.DuplexStreamingServerHandler(requestStream, responseStream, context, continuation);

            logger.Log(LogLevel.Information, $"End - {context.Method}");
        }
    }
}
