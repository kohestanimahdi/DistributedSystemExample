using DistributedSystems.Web.Dtos.Requests;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace DistributedSystems.Web.Data
{

    public class grpcServices
    {
        private Flight.Flights.FlightsClient client;

        public grpcServices()
        {
            Channel channel = new Channel("127.0.0.1:30051", ChannelCredentials.Insecure);
            client = new Flight.Flights.FlightsClient(channel);
        }
        public async Task<Flight.GetAirPortsResponse> GetAirportsAsync()
        {
            try
            {
                var airports = await client.GetAirportsListAsync(new Flight.GetAirPortsRequest { International = false, Title = "" });
                return airports;
            }
            catch (Exception ex)
            {
                throw;
            }

            return null;
        }


        public async Task<Flight.GetFlightsResponse> GetFlightsAsync(SearchFlightRequest request)
        {
            try
            {
                var flights = await client.GetFlightsListAsync(new Flight.GetFlightsRequest
                {
                    DepartureDate = request.DepartureDate.ToString("yyyy-MM-dd"),
                    DestinationCode = request.DestinationCode,
                    AdultCount = 1,
                    ChildCount = 0,
                    InfantCount = 0,
                    OriginCode = request.OriginCode,
                    PageNumber = 1,
                    PageSize = 10000
                });
                return flights;
            }
            catch (Exception ex)
            {
                throw;
            }

            return null;
        }
    }
}
