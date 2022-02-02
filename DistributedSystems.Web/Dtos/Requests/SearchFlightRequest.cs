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
            AdultCount = 1;
            ChildCount = 0;
            InfantCount = 0;
        }
        public string OriginCode { get; set; }
        public string DestinationCode { get; set; }
        public DateTime DepartureDate { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public int InfantCount { get; set; }
    }
}
