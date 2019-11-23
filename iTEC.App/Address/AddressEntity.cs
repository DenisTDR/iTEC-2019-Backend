using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Base.Web.Base.Attributes;
using API.Base.Web.Base.Models.Entities;

namespace iTEC.App.Address
{
    public class AddressEntity : Entity
    {
        [DataType(DataType.MultilineText)] public string Address { get; set; }
        public string City { get; set; }

        public float LocationLat
        {
            get => Location?.Lat ?? 0;
            set => Location = new GeoLocation(value, Location?.Long ?? 0);
        }

        public float LocationLong
        {
            get => Location?.Long ?? 0;
            set => Location = new GeoLocation(Location?.Lat ?? 0, value);
        }

        [IsReadOnly] [NotMapped] public GeoLocation Location { get; set; }

        public override string ToString()
        {
            return Address + "," + City + " (" + LocationLat + ", " + LocationLong + ")";
        }
    }
}