using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace DistributedSystems.Web.Data
{
    public class grpcServices
    {
        public async Task<Flight.GetAirPortsResponse> GetAirportsAsync()
        {
            try
            {
                Channel channel = new Channel("127.0.0.1:30051", ChannelCredentials.Insecure);
                var client = new Flight.Flights.FlightsClient(channel);

                var airports = await client.GetAirportsListAsync(new Flight.GetAirPortsRequest { International = false, Title = "" });
                return airports;
            }
            catch (Exception ex)
            {
                throw;
            }

            return null;
        }
    }
}
