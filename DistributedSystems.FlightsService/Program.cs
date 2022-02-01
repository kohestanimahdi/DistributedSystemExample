using DistributedSystems.FlightsService.ProtoImplementations;
using Flight;
using Grpc.Core;
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
                Server server = new Server
                {
                    Services = { Flight.Flights.BindService(new FlightProtoImpl()) },
                    Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
                };
                server.Start();

                Console.WriteLine("Greeter server listening on port " + Port);
                Console.WriteLine("Press any key to stop the server...");
                Console.ReadKey();

                server.ShutdownAsync().Wait();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
