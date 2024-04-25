using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetFinal_2147037.Models
{
    [Table("changelog", Schema = "Personne")]
    public partial class Changelog
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("type")]
        public byte? Type { get; set; }
        [Column("version")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Version { get; set; }
        [Column("description")]
        [StringLength(200)]
        [Unicode(false)]
        public string Description { get; set; } = null!;
        [Column("name")]
        [StringLength(300)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("checksum")]
        [StringLength(32)]
        [Unicode(false)]
        public string? Checksum { get; set; }
        [Column("installed_by")]
        [StringLength(100)]
        [Unicode(false)]
        public string InstalledBy { get; set; } = null!;
        [Column("installed_on", TypeName = "datetime")]
        public DateTime InstalledOn { get; set; }
        [Column("success")]
        public bool Success { get; set; }
    }
}
