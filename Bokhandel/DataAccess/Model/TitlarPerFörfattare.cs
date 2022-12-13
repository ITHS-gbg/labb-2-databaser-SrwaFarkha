using System;
using System.Collections.Generic;

namespace Bokhandel.Model
{
    public partial class TitlarPerFörfattare
    {
        public string Namn { get; set; } = null!;
        public string? Ålder { get; set; }
        public string? Titlar { get; set; }
        public decimal? Lagervärde { get; set; }
    }
}
