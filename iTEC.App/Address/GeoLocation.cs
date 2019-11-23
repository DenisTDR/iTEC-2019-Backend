namespace iTEC.App.Address
{
    public struct GeoLocation
    {
        public GeoLocation(float lat, float _long)
        {
            Lat = lat;
            Long = _long;
        }

        public readonly float Lat;
        public readonly float Long;
    }
}