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

        public float LocationX
        {
            get => Location.X;
            set => Location = new PointF(value, Location.Y);
        }

        public float LocationY
        {
            get => Location.Y;
            set => Location = new PointF(Location.X, value);
        }

        [IsReadOnly] [NotMapped] public PointF Location { get; set; }

        public override string ToString()
        {
            return Address + " (" + LocationX + ", " + LocationY + ")";
        }
    }
}