using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjetFinal_2147037.Models
{
    public class VM_UtilisateurMotDePasse
    {

        [StringLength(25)]
        public string Pseudo { get; set; } = null!;

        [StringLength(10)]
        [Unicode(false)]
        public string NoTelephone { get; set; } = null!;
        public string? MotDePasseHache { get; set; }

        [Column("PlateformeID")]
        public int PlateformeId { get; set; }
    }
}
