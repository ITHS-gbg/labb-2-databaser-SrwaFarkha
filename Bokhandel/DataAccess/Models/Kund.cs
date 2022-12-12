using System;
using System.Collections.Generic;

namespace Bokhandel.Models
{
    public partial class Kund
    {
        public Kund()
        {
            Ordrars = new HashSet<Ordrar>();
        }

        public int KundId { get; set; }
        public string Förnamn { get; set; } = null!;
        public string Efternamn { get; set; } = null!;
        public string Adress { get; set; } = null!;
        public int Postnummer { get; set; }
        public string Stad { get; set; } = null!;
        public int Telefonummer { get; set; }

        public virtual ICollection<Ordrar> Ordrars { get; set; }
    }
}
