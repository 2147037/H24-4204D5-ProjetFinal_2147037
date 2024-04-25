using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetFinal_2147037.Models
{
    [Table("Plateforme", Schema = "Television")]
    public partial class Plateforme
    {
        public Plateforme()
        {
            EmissionTelevisions = new HashSet<EmissionTelevision>();
            Utilisateurs = new HashSet<Utilisateur>();
        }

        [Key]
        [Column("PlateformeID")]
        public int PlateformeId { get; set; }
        [StringLength(35)]
        public string Nom { get; set; } = null!;
        [Column(TypeName = "text")]
        public string Descriptions { get; set; } = null!;
        public int NbEmissionCoreenne { get; set; }

        [InverseProperty("Plateforme")]
        public virtual ICollection<EmissionTelevision> EmissionTelevisions { get; set; }
        [InverseProperty("Plateforme")]
        public virtual ICollection<Utilisateur> Utilisateurs { get; set; }
    }
}
