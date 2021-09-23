using Grpc.Core;
using Grpc.Net.Client;
using GrpsService;
using System;
using System.Threading.Tasks;

namespace GrpsClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string serverUrl = "https://localhost:54295";
            using(var channel = GrpcChannel.ForAddress(serverUrl)) {
                var client = new Greeter.GreeterClient(channel);

                // пПростой клиент
                var reply = await client.SayHelloAsync(new HelloRequest() { Name = "World" });
                Console.WriteLine($"Hello {reply.Message}");

                // Стриминговый клиент
                using (var call = client.SayHelloStream()) {
                    var readTask = Task.Run(async () => {
                        await foreach(var response in call.ResponseStream.ReadAllAsync()) {
                            Console.WriteLine(response.Message);
                        }
                    });

                    while(true) {
                        var result = Console.ReadLine();

                        if(string.IsNullOrWhiteSpace(result))
                            break;

                        await call.RequestStream.WriteAsync(new HelloRequest { Name = result });
                    }

                    await call.RequestStream.CompleteAsync();
                    await readTask;
                }

                Console.ReadLine();
            }
        }
    }
}
