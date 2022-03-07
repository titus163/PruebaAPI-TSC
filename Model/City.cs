using System;
using System.Collections.Generic;

#nullable disable

namespace Prueba.Model
{
    public partial class City
    {
        public int CityId { get; set; }
        public string Name { get; set; }
        public int? StateId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string WikiDataId { get; set; }

        public virtual States State { get; set; }
    }

    public partial class City_Item
    {
        public int CityId { get; set; }
        public string Name { get; set; }
        public int? StateId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string WikiDataId { get; set; }

    }
}
