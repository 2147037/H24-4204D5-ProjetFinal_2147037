using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetFinal_2147037.Models
{
    [Keyless]
    [Table("MotDePasse", Schema = "Personne")]
    public partial class MotDePasse
    {
        [Column("MotDePasse")]
        [StringLength(40)]
        public string? MotDePasse1 { get; set; }
    }
}
