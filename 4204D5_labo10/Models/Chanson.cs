using System;
using System.Collections.Generic;

namespace _4204D5_labo10.Models
{
    public partial class Chanson
    {
        public int ChansonId { get; set; }
        public string Nom { get; set; } = null!;
        public int? ChanteurId { get; set; }

        public virtual Chanteur? Chanteur { get; set; }
    }
}
