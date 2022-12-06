using System;
using System.Collections.Generic;

namespace Bokhandel.Models
{
    public partial class Ordrar
    {
        public int OrderId { get; set; }
        public int KundId { get; set; }
        public DateTime OrderDatum { get; set; }
        public string LeveransAdress { get; set; } = null!;
        public string Stad { get; set; } = null!;
        public int Postnummer { get; set; }
        public DateTime LeveransDatum { get; set; }
        public int ButikId { get; set; }

        public virtual Butiker Butik { get; set; } = null!;
        public virtual Kund Kund { get; set; } = null!;
    }
}
