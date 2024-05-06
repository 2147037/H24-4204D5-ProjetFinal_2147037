using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace ProjetFinal_2147037.Models
{
    public class ImageUploadVM
    {
        [Required(ErrorMessage = "Il faut joindre un fichier image.")]
        public IFormFile FormFile { get; set; } = null!;

        public Utilisateur? User { get; set; }

        [Required(ErrorMessage = "Il faut spécifier le nom d'utilisateur.")]
        [DisplayName("Nom de l'utilisateur")]
        public string NomUtil { get; set; } = null!;
    }
}
