using Flight;
using Google.Protobuf.Collections;
using Grpc.Core;
using MrBilitFlightService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DistributedSystems.FlightsService.ProtoImplementations
{
    internal class FlightProtoImpl : Flights.FlightsBase
    {
        private MrBilitFlight mrBilitFlight;

        public FlightProtoImpl()
        {
            mrBilitFlight = new MrBilitFlight("https://flight-api.mrbilit.com/", new System.Net.Http.HttpClient());
        }
        // Server side handler of the SayHello RPC
        public override async Task<GetFlightsResponse> GetFlightsList(GetFlightsRequest request, ServerCallContext context)
        {
            var requestModel = new GetFlightsRequestDtoTHP
            {
                PageSize = request.PageSize,
                PageNumber = request.PageNumber,
                InfantCount = request.InfantCount,
                ChildCount = request.ChildCount,
                AdultCount = request.AdultCount,
                CabinClass = CabinClassRequest._0,
                Routes = new List<FlightRouteDtoTHP>
                {
                    new FlightRouteDtoTHP
                    {
                        DepartureDate = Convert.ToDateTime(request.DepartureDate),
                        DestinationCode = request.DestinationCode,
                        OriginCode = request.OriginCode
                    }
                }
            };
            var flights = await mrBilitFlight.FlightsAsync(requestModel);
            var response = new GetFlightsResponse();
            response.Meta = JsonConvert.DeserializeObject<GetFlightsResponse.Types.META>(JsonConvert.SerializeObject(flights.Meta));
            response.Flights.Add(JsonConvert.DeserializeObject<RepeatedField<GetFlightsResponse.Types.FLIGHTS>>(JsonConvert.SerializeObject(flights.Flights)));

            return response;
        }

        public override async Task<GetAirPortsResponse> GetAirportsList(GetAirPortsRequest request, ServerCallContext context)
        {
            var airPorts = await mrBilitFlight.AirportsAsync(request.Title, request.International);
            var response = new GetAirPortsResponse();
            response.CityAirports.Add(JsonConvert.DeserializeObject<RepeatedField<GetAirPortsResponse.Types.CITYAIRPORT>>(JsonConvert.SerializeObject(airPorts)));

            return response;
        }
    }
}
