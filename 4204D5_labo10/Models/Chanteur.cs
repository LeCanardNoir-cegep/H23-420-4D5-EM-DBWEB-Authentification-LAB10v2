using System;
using System.Collections.Generic;

namespace _4204D5_labo10.Models
{
    public partial class Chanteur
    {
        public Chanteur()
        {
            Chansons = new HashSet<Chanson>();
        }

        public string Nom { get; set; } = null!;
        public DateTime DateNaissance { get; set; }
        public int ChanteurId { get; set; }

        public virtual ICollection<Chanson> Chansons { get; set; }
    }
}
