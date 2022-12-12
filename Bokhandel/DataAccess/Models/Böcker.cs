using System;
using System.Collections.Generic;
using Console = System.Console;

namespace Bokhandel.Models
{
    public partial class Böcker
    {
        public string Isbn13 { get; set; } = null!;
        public string Titel { get; set; } = null!;
        public string Språk { get; set; } = null!;
        public decimal Pris { get; set; }
        public DateTime Utgivningsdatum { get; set; }
        public int FörfattarId { get; set; }

        public virtual Författare Författar { get; set; } = null!;


    }
}
