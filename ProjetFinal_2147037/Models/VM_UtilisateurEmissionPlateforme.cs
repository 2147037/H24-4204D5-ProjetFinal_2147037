using System.ComponentModel.DataAnnotations;

namespace ProjetFinal_2147037.Models
{
    public class VM_UtilisateurEmissionPlateforme
    {
        [StringLength(50)]
        public string Nom { get; set; } = null!;

        public int NbVisionnement { get; set; }
        public bool EstCoreen { get; set; }
    }
}
