using System;
using System.Collections.Generic;

namespace _4204D5_labo10.Models
{
    public partial class VwChanteurNbChanson
    {
        public int ChanteurId { get; set; }
        public string Nom { get; set; } = null!;
        public DateTime DateDeNaissance { get; set; }
        public int? NombreDeChansons { get; set; }
    }
}
