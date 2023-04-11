using System.ComponentModel.DataAnnotations;

namespace _4204D5_labo10.ViewModels
{
    public class ConnexionViewModel
    {
        [Required(ErrorMessage = "Veuillez préciser un nom d'utilisateur.")]
        public string Pseudo { get; set; } = null!;

        [Required(ErrorMessage = "Veuillez entrer un mot de passe.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Le mot de passe doit avoir entre 6 et 50 caractères.")]
        [DataType(DataType.Password)]
        public string MotDePasse { get; set; } = null!;
    }
}
