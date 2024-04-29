using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetFinal_2147037.Models
{
    [Table("Utilisateur", Schema = "Personne")]
    [Index("NoTelephone", Name = "UC_Utilisateur_NoTelephone", IsUnique = true)]
    [Index("Pseudo", Name = "UC_Utilisateur_Pseudo", IsUnique = true)]
    public partial class Utilisateur
    {
        public Utilisateur()
        {
            Adresses = new HashSet<Adresse>();
            Courriels = new HashSet<Courriel>();
        }

        [Key]
        [Column("UtilisateurID")]
        public int UtilisateurId { get; set; }
        [StringLength(25)]
        public string Pseudo { get; set; } = null!;
        [StringLength(10)]
        [Unicode(false)]
        public string NoTelephone { get; set; } = null!;
        public byte[]? MotDePasseHache { get; set; }
        [Column("PlateformeID")]
        public int PlateformeId { get; set; }

        [ForeignKey("PlateformeId")]
        [InverseProperty("Utilisateurs")]
        public virtual Plateforme Plateforme { get; set; } = null!;
        [InverseProperty("Utilisateur")]
        public virtual ICollection<Adresse> Adresses { get; set; }
        [InverseProperty("Utilisateur")]
        public virtual ICollection<Courriel> Courriels { get; set; }
    }
}
