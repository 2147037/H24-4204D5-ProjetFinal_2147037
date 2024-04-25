using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetFinal_2147037.Models
{
    [Table("Acteur", Schema = "Personne")]
    public partial class Acteur
    {
        public Acteur()
        {
            Personnages = new HashSet<Personnage>();
        }

        [Key]
        [Column("ActeurID")]
        public int ActeurId { get; set; }
        [StringLength(15)]
        public string Nom { get; set; } = null!;
        [StringLength(15)]
        public string Prenom { get; set; } = null!;
        public int Age { get; set; }

        [InverseProperty("Acteur")]
        public virtual ICollection<Personnage> Personnages { get; set; }
    }
}
