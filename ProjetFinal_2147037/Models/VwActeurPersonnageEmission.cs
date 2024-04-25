using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetFinal_2147037.Models
{
    [Keyless]
    public partial class VwActeurPersonnageEmission
    {
        [Column("ActeurID")]
        public int ActeurId { get; set; }
        [StringLength(15)]
        public string Nom { get; set; } = null!;
        [StringLength(15)]
        public string Prenom { get; set; } = null!;
        [Column("PersonnageID")]
        public int PersonnageId { get; set; }
        [StringLength(15)]
        public string NomPersonnage { get; set; } = null!;
        [Column("EmissionTelevisionID")]
        public int EmissionTelevisionId { get; set; }
        [StringLength(50)]
        public string NomEmission { get; set; } = null!;
        public int Cote { get; set; }
    }
}
