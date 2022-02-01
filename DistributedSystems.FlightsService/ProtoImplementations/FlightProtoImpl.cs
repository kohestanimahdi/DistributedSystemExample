using Flight;
using Google.Protobuf.Collections;
using Grpc.Core;
using MrBilitFlightService;
using Newtonsoft.Json;
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
            return null;
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
