using DistributedSystems.Common.Utilities;
using DistributedSystems.Web.Dtos.Requests;
using Grpc.Core;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;

namespace DistributedSystems.Web.Data
{

    public class grpcServices
    {
        private readonly IDistributedCache cache;
        private Flight.Flights.FlightsClient client;

        public grpcServices(IDistributedCache cache)
        {
            Channel channel = new Channel("127.0.0.1:30051", ChannelCredentials.Insecure);
            client = new Flight.Flights.FlightsClient(channel);
            this.cache = cache;
        }


        public async Task<Flight.GetAirPortsResponse> GetAirportsAsync()
        {
            var airports = await cache.GetValueAsync<Flight.GetAirPortsResponse>("AirPortsResponse");
            if (airports is not null)
                return airports;

            try
            {
                airports = await client.GetAirportsListAsync(new Flight.GetAirPortsRequest { International = false, Title = "" });
                await cache.SetValueAsync("AirPortsResponse", airports, 30);
                return airports;
            }
            catch { }

            return null;
        }
        public async Task<Flight.GetFlightsResponse> GetFlightsAsync(SearchFlightRequest request)
        {
            var flights = await cache.GetValueAsync<Flight.GetFlightsResponse>(request.ToString());
            if (flights is not null)
                return flights;

            try
            {
                flights = await client.GetFlightsListAsync(new Flight.GetFlightsRequest
                {
                    DepartureDate = request.DepartureDate.ToString("yyyy-MM-dd"),
                    DestinationCode = request.DestinationCode,
                    AdultCount = request.AdultCount,
                    ChildCount = request.ChildCount,
                    InfantCount = request.InfantCount,
                    OriginCode = request.OriginCode,
                    PageNumber = 1,
                    PageSize = 10000
                });

                await cache.SetValueAsync(request.ToString(), flights, 2);

                return flights;
            }
            catch
            {
            }

            return null;
        }
    }
}
