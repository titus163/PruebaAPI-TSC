using System;
using System.Collections.Generic;

#nullable disable

namespace Prueba.Model
{
    public partial class Timezone
    {
        public int TimezonesId { get; set; }
        public int? CountryId { get; set; }
        public string ZoneName { get; set; }
        public int? GmtOffset { get; set; }
        public string GmtOffsetName { get; set; }
        public string Abbreviation { get; set; }
        public string TzName { get; set; }

        public virtual Country Country { get; set; }
    }
}
