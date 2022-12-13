using System;
using System.Collections.Generic;

namespace Bokhandel.Model
{
    public partial class LagerSaldo
    {
        public int Id { get; set; }
        public string Isbn { get; set; } = null!;
        public int ButikId { get; set; }
        public int Antal { get; set; }

        public virtual Butiker Butik { get; set; } = null!;
        public virtual Böcker IsbnNavigation { get; set; } = null!;
    }
}
