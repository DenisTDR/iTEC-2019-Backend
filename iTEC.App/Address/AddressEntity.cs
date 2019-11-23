using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using API.Base.Web.Base.Attributes;
using API.Base.Web.Base.Models.Entities;

namespace iTEC.App.Address
{
    public class AddressEntity : Entity
    {
        [DataType(DataType.MultilineText)] public string Address { get; set; }

        public float LocationLat
        {
            get => Location.Lat;
            set => Location = new GeoLocation(value, Location.Long);
        }

        public float LocationLong
        {
            get => Location.Long;
            set => Location = new GeoLocation(Location.Lat, value);
        }

        [IsReadOnly] [NotMapped] public GeoLocation Location { get; set; }

        public override string ToString()
        {
            return Address + " (" + LocationLat + ", " + LocationLong + ")";
        }
    }
}