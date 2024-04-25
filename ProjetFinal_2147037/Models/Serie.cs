using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetFinal_2147037.Models
{
    [Table("Serie", Schema = "Television")]
    public partial class Serie
    {
        [Key]
        [Column("SerieID")]
        public int SerieId { get; set; }
        public int NbEpisode { get; set; }
        [Column("EmissionTelevisionID")]
        public int EmissionTelevisionId { get; set; }

        [ForeignKey("EmissionTelevisionId")]
        [InverseProperty("Series")]
        public virtual EmissionTelevision EmissionTelevision { get; set; } = null!;
    }
}
