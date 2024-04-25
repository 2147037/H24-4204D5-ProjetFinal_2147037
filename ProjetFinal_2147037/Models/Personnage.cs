using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetFinal_2147037.Models
{
    [Table("Personnage", Schema = "Personne")]
    public partial class Personnage
    {
        [Key]
        [Column("PersonnageID")]
        public int PersonnageId { get; set; }
        [StringLength(15)]
        public string Nom { get; set; } = null!;
        [Column("ActeurID")]
        public int ActeurId { get; set; }
        [Required]
        public bool? EstVivant { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateMort { get; set; }
        [Column("EmissionTelevisionID")]
        public int EmissionTelevisionId { get; set; }

        [ForeignKey("ActeurId")]
        [InverseProperty("Personnages")]
        public virtual Acteur Acteur { get; set; } = null!;
        [ForeignKey("EmissionTelevisionId")]
        [InverseProperty("Personnages")]
        public virtual EmissionTelevision EmissionTelevision { get; set; } = null!;
    }
}
