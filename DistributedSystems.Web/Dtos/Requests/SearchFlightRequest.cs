using System;

namespace DistributedSystems.Web.Dtos.Requests
{
    public class SearchFlightRequest
    {
        public SearchFlightRequest()
        {
            OriginCode = "THR";
            DestinationCode = "MHD";
            DepartureDate = DateTime.Now;

        }
        public string OriginCode { get; set; }
        public string DestinationCode { get; set; }
        public DateTime DepartureDate { get; set; }
    }
}
