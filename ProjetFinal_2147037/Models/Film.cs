using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetFinal_2147037.Models
{
    [Table("Film", Schema = "Television")]
    public partial class Film
    {
        [Key]
        [Column("FilmID")]
        public int FilmId { get; set; }
        public int DureeMinute { get; set; }
        [Column("EmissionTelevisionID")]
        public int EmissionTelevisionId { get; set; }

        [ForeignKey("EmissionTelevisionId")]
        [InverseProperty("Films")]
        public virtual EmissionTelevision EmissionTelevision { get; set; } = null!;
    }
}
