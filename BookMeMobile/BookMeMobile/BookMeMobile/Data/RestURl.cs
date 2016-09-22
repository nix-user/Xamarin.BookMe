using BookMeMobile.Resources;

namespace BookMeMobile.Data
{
    public static class RestURl
    {
        private static string apiURL = URIResources.IpAdress + URIResources.ApiURL;

        public static string BookURI => apiURL + URIResources.ReservationDomainURL;

        public static string RoomURl => apiURL + URIResources.RoomDomainURL;

        public static string GetEmptyRoom => RoomURl + URIResources.EmptyRoomURL;

        public static string GetCurrentRoomReservation => RoomURl + URIResources.CurrentRoomURL;

        public static string GetUserReservation => BookURI;

        public static string GetToken => URIResources.IpAdress + URIResources.Token;
    }
}