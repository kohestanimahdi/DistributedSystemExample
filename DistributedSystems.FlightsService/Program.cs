using DistributedSystems.FlightsService.ProtoImplementations;
using Flight;
using Grpc.Core;
using Grpc.Core.Logging;
using System;
using System.Threading.Tasks;

namespace DistributedSystems.FlightsService
{
    internal class Program
    {
        const int Port = 30051;

        public static void Main(string[] args)
        {
            try
            {
                GrpcEnvironment.SetLogger(new ConsoleLogger());

                Server server = new Server
                {
                    Services = { Flights.BindService(new FlightProtoImpl()) },
                    Ports = { new ServerPort("0.0.0.0", Port, ServerCredentials.Insecure),
                        new ServerPort("127.0.0.1", Port + 1, ServerCredentials.Insecure) ,
                        new ServerPort("localhost", Port + 2, ServerCredentials.Insecure) }

                };
                server.Start();

                Console.WriteLine("Greeter server listening on port " + Port);
                Console.WriteLine("Press any key to stop the server...");

                while (true)
                    Task.Delay(Int32.MaxValue);

                server.ShutdownAsync().Wait();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
