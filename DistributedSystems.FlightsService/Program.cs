using Flight;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace DistributedSystems.FlightsService
{
    class FlightImpl : Flight.Flights.FlightsBase
    {
        // Server side handler of the SayHello RPC
        public override Task<GetFlightsResponse> GetFlightsList(GetFlightsRequest request, ServerCallContext context)
        {
            return base.GetFlightsList(request, context);
        }
    }
    internal class Program
    {
        const int Port = 30051;

        public static void Main(string[] args)
        {
            Server server = new Server
            {
                Services = { Flight.Flights.BindService(new FlightImpl()) },
                Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
            };
            server.Start();

            Console.WriteLine("Greeter server listening on port " + Port);
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();

            server.ShutdownAsync().Wait();
        }
    }
}
