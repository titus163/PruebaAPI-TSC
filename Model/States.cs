using System;
using System.Collections.Generic;

#nullable disable

namespace Prueba.Model
{
    public partial class States
    {
        public States()
        {
            Cities = new HashSet<City>();
        }

        public int StateId { get; set; }
        public string Name { get; set; }
        public int? CountryId { get; set; }
        public string StateCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<City> Cities { get; set; }
    }

    public partial class States_Item
    {
        public int StateId { get; set; }
        public string Name { get; set; }
        public int? CountryId { get; set; }
        public string StateCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

    }
}
