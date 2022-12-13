using System;
using System.Collections.Generic;

namespace Bokhandel.Model
{
    public partial class Butiker
    {
        public Butiker()
        {
            LagerSaldos = new HashSet<LagerSaldo>();
            Ordrars = new HashSet<Ordrar>();
        }

        public int ButikId { get; set; }
        public string Namn { get; set; } = null!;
        public string Adress { get; set; } = null!;
        public int Postnummer { get; set; }
        public string Stad { get; set; } = null!;

        public virtual ICollection<LagerSaldo> LagerSaldos { get; set; }
        public virtual ICollection<Ordrar> Ordrars { get; set; }
    }
}
