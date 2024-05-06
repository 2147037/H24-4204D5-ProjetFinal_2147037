using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetFinal_2147037.Models
{
    [Table("EmissionTelevision", Schema = "Television")]
    [Index("PlateformeId", "EstCoreen", Name = "IX_EmissionTelevision_PlateformeIDEstCoreen")]
    public partial class EmissionTelevision
    {
        public EmissionTelevision()
        {
            Films = new HashSet<Film>();
            Personnages = new HashSet<Personnage>();
            Series = new HashSet<Serie>();
        }

        [Key]
        [Column("EmissionTelevisionID")]
        public int EmissionTelevisionId { get; set; }
        [StringLength(50)]
        public string Nom { get; set; } = null!;
        [Column(TypeName = "text")]
        public string Descriptions { get; set; } = null!;
        public bool EstCoreen { get; set; }
        public int Cote { get; set; }
        public int NbVisionnement { get; set; }
        [Column(TypeName = "money")]
        public decimal CoutProduction { get; set; }
        [Column("PlateformeID")]
        public int PlateformeId { get; set; }

        [ForeignKey("PlateformeId")]
        [InverseProperty("EmissionTelevisions")]
        public virtual Plateforme Plateforme { get; set; } = null!;
        [InverseProperty("EmissionTelevision")]
        public virtual ICollection<Film> Films { get; set; }
        [InverseProperty("EmissionTelevision")]
        public virtual ICollection<Personnage> Personnages { get; set; }
        [InverseProperty("EmissionTelevision")]
        public virtual ICollection<Serie> Series { get; set; }
    }
}
