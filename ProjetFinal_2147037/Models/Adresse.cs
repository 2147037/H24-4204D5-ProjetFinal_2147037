using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetFinal_2147037.Models
{
    [Table("Adresse", Schema = "Personne")]
    public partial class Adresse
    {
        [Key]
        [Column("AdresseID")]
        public int AdresseId { get; set; }
        public int NoPorte { get; set; }
        [StringLength(55)]
        public string Rue { get; set; } = null!;
        public int? NoAppartement { get; set; }
        [StringLength(50)]
        public string Ville { get; set; } = null!;
        [StringLength(7)]
        public string CodePostal { get; set; } = null!;
        [StringLength(25)]
        public string Province { get; set; } = null!;
        [StringLength(20)]
        public string Pays { get; set; } = null!;
        [Column("UtilisateurID")]
        public int UtilisateurId { get; set; }

        [ForeignKey("UtilisateurId")]
        [InverseProperty("Adresses")]
        public virtual Utilisateur Utilisateur { get; set; } = null!;
    }
}
