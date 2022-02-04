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


        public static void Main(string[] args)
        {
            int Port = 0;

            var GRPC_SERVER_PORT = Environment.GetEnvironmentVariable("GRPC_SERVER_PORT");
            if(string.IsNullOrWhiteSpace(GRPC_SERVER_PORT) || !int.TryParse(GRPC_SERVER_PORT, out Port))
                Port = 30051;

            Task.Run(() =>
            {
                GrpcEnvironment.SetLogger(new ConsoleLogger());

                Server server = new Server
                {
                    Services = { Flights.BindService(new FlightProtoImpl()) },
                    Ports = { new ServerPort("0.0.0.0", Port, ServerCredentials.Insecure) }

                };
                server.Start();

                Console.WriteLine("Greeter server listening on port " + Port);
            });

            while (true)
                Task.Delay(Int32.MaxValue);

            //server.ShutdownAsync().Wait();
        }
    }
}
