using System;
using System.Collections.Generic;

namespace Bokhandel.Model
{
    public partial class Författare
    {
        public Författare()
        {
            Böckers = new HashSet<Böcker>();
        }

        public int Id { get; set; }
        public string Förnamn { get; set; } = null!;
        public string Efternamn { get; set; } = null!;
        public DateTime Födelsedatum { get; set; }

        public virtual ICollection<Böcker> Böckers { get; set; }
    }
}
