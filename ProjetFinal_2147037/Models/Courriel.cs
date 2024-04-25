using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetFinal_2147037.Models
{
    [Table("Courriel", Schema = "Personne")]
    public partial class Courriel
    {
        [Key]
        [Column("CourrielID")]
        public int CourrielId { get; set; }
        [StringLength(100)]
        public string Nom { get; set; } = null!;
        [Column("UtilisateurID")]
        public int UtilisateurId { get; set; }

        [ForeignKey("UtilisateurId")]
        [InverseProperty("Courriels")]
        public virtual Utilisateur Utilisateur { get; set; } = null!;
    }
}
