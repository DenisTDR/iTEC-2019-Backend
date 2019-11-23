namespace iTEC.App.Address
{
    public class GeoLocation
    {
        public GeoLocation()
        {
        }

        public GeoLocation(float lat, float lng)
        {
            Lat = lat;
            Lng = lng;
        }

        public float Lat { get; set; }
        public float Lng { get; set; }
    }
}