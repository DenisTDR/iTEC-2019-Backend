namespace iTEC.App.Address
{
    public class GeoLocation
    {
        public GeoLocation()
        {
        }

        public GeoLocation(float lat, float _long)
        {
            Lat = lat;
            Long = _long;
        }

        public float Lat { get; set; }
        public float Long { get; set; }
    }
}