using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace _4204D5_labo10.Models
{
    [Table("Chanson", Schema = "Musique")]
    public partial class Chanson
    {
        [Key]
        [Column("ChansonID")]
        public int ChansonId { get; set; }
        [StringLength(100)]
        public string Nom { get; set; } = null!;
        [StringLength(50)]
        public string NomChanteur { get; set; } = null!;

        [ForeignKey("NomChanteur")]
        [InverseProperty("Chansons")]
        public virtual Chanteur NomChanteurNavigation { get; set; } = null!;
    }
}
